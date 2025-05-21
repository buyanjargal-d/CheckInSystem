using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CheckInServer.Socket.Services;
using Xunit;


namespace CheckInSystem.Tests
{
    /// <summary>
    /// SeatSocketService-ийн интеграц болон клиент-серверийн хоорондын харилцааг шалгах тестүүд.
    /// </summary>
    public class SeatSocketServiceIntegrationTests
    {

        /// <summary>
        /// Клиент-серверийг хамтад нь ажиллуулж, "Assign" төрлийн JSON илгээхэд сервер "ACK" гэсэн хариу
        /// буцааж байгаа эсэхийг шалгана.
        /// </summary>
        [Fact]
        public async Task TestServerAndClientTogether()
        {
            // Серверийг эхлүүлэх
            var server = new SeatSocketService();
            server.Start();

            // Клиентээс холбогдох
            var client = new TcpClient("localhost", 5050);
            var stream = client.GetStream();
            var json = "{\"type\":\"Assign\",\"seatId\":1,\"passengerId\":101}";
            var bytes = Encoding.UTF8.GetBytes(json);
            await stream.WriteAsync(bytes, 0, bytes.Length);

            // Хариуг унших нь 
            var buffer = new byte[1024];
            int read = await stream.ReadAsync(buffer, 0, buffer.Length);
            var response = Encoding.UTF8.GetString(buffer, 0, read);

            Assert.Equal("ACK", response);
        }

        /// <summary>
        /// HandleClientAsync функц дотроо "Assign" төрлийн мессежийг зөв бүртгэж, 
        /// клиент рүү "ACK" буцааж буйг шалгана. 
        /// Reflection ашиглан private функц рүү хандана болно.
        /// </summary>
        [Fact]
        public async Task HandleClientAsync_ShouldLogAssignMessage_AndRespondAck()
        {
            //Серверийг боломжтой port дээр эхлүүлж байна (0 = OS дурын port өгнө)
            var server = new TcpListener(IPAddress.Loopback, 0);
            server.Start(); 
            int port = ((IPEndPoint)server.LocalEndpoint).Port;
            //Клиент үүргийг тусдаа thread/task дээр эхлүүлж, сервертэй холбогдож, мессеж илгээх
            var clientTask = Task.Run(async () =>
            {
                using var client = new TcpClient();
                await client.ConnectAsync(IPAddress.Loopback, port);
                using var stream = client.GetStream();
                // Илгээх JSON мессеж: төрөл нь Assign, суудал ба зорчигчийн ID-г өгнө
                var message = new
                {
                    type = "Assign",
                    seatId = 100,
                    passengerId = 200
                };
                // JSON рүү хөрвүүлж UTF-8 форматтайгаар сервер рүү илгээнэ
                var json = JsonSerializer.Serialize(message, new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() }
                });

                var bytes = Encoding.UTF8.GetBytes(json);
                await stream.WriteAsync(bytes, 0, bytes.Length);
                // Серверээс хариу (ACK) уншина
                var buffer = new byte[1024];
                var read = await stream.ReadAsync(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer, 0, read);
            });
            // Сервер талд клиент холбогдсон эсэхийг шалгаж, хүлээж авна
            var client = await server.AcceptTcpClientAsync();
            // SeatSocketService классыг үүсгэж, доторх private HandleClientAsync функцэд reflection ашиглан хандана
            var service = new SeatSocketService();
            var method = typeof(SeatSocketService)
                .GetMethod("HandleClientAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.NotNull(method); // Хэрвээ олдоогүй бол тест fail болно

            var task = (Task)method.Invoke(service, new object[] { client })!;
            await task;

            var ack = await clientTask;
            Assert.Equal("ACK", ack);// Сервер ACK хариу өгсөн байх ёстой

            server.Stop();
        }

      
        /// <summary>
        /// HandleClientAsync() функц буруу JSON хүлээн авсан үед:
        /// - Алдаа шидэлгүйгээр ("try-catch" ашиглан) эмх цэгцтэйгээр хүлээн авсан мессежийг log хийж
        /// - Клиент рүү "ACK" хариу илгээж байгаа эсэхийг шалгана.
        /// Энэ нь robustness буюу найдвартай ажиллагааг шалгаж буй тест.
        /// </summary>
        [Fact]
        public async Task HandleClientAsync_ShouldLogError_WhenInvalidJsonReceived()
        {
            var server = new TcpListener(IPAddress.Loopback, 0);
            server.Start();
            int port = ((IPEndPoint)server.LocalEndpoint).Port;

            // Клиентийг тусдаа task дээр эхлүүлж, сервер рүү буруу JSON илгээнэ
            var clientTask = Task.Run(async () =>
            {
                using var client = new TcpClient();
                await client.ConnectAsync(IPAddress.Loopback, port); // Сервертэй холбогдох
                using var stream = client.GetStream();

                // Буруу бүтэцтэй JSON өгөгдөл илгээх
                var badJson = Encoding.UTF8.GetBytes("{ invalid_json }");
                await stream.WriteAsync(badJson, 0, badJson.Length);

                // Серверээс ирж буй хариуг буцаан уншина
                var buffer = new byte[1024];
                int read = await stream.ReadAsync(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer, 0, read);
            });

            //Сервер талд клиент холбогдсон client socket-ийг авна
            var service = new SeatSocketService();
            var client = await server.AcceptTcpClientAsync();

            // HandleClientAsync (private async function)-ийг reflection ашиглан олж ажиллуулна
            var method = typeof(SeatSocketService)
                .GetMethod("HandleClientAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            var task = (Task)method.Invoke(service, new object[] { client });
            await task;

            //Клиентээс ирсэн хариу ACK байгаа эсэхийг шалгана
            var response = await clientTask;
            Assert.Equal("ACK", response); // Буруу JSON байсан ч ACK буцааж байгаа эсэх

            server.Stop();
        }
    }
}
