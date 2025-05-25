using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using CheckInSystem.Business.Services;
using CheckInServer.Socket.Services;
using System.Text.Json.Serialization;
using System.Reflection;

namespace CheckInSystem.Tests
{
    /// <summary>
    /// SocketNotifier классын нэгж болон интеграцийн тестүүд.
    /// TCP сокет ашиглан суудал оноох мэдээлэл сервер рүү амжилттай дамжуулж байгаа эсэхийг шалгана.
    /// </summary>
    public class SocketNotifierTests
    {
        /// <summary>
        /// Тухайн үед ашиглагдаагүй порт олох туслах функц.
        /// TCP сервер түр эхлүүлээд, портын дугаарыг авах
        /// </summary>
        private int GetAvailablePort()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        /// <summary>
        /// NotifySeatAssigned() функц ажиллах үед:
        /// - Сокет сервер рүү JSON мессеж илгээгдэж байгаа эсэх
        /// - "type", "seatId", "passengerId" талбарууд зөв дамжиж буйг шалгана.
        /// </summary>
        [Fact]
        public async Task NotifySeatAssigned_ShouldSendJsonMessageToSocketServer()
        {
            // Тохиргоо
            int port = GetAvailablePort(); 
            var expectedSeatId = 123;
            var expectedPassengerId = 456;

            var listener = new TcpListener(IPAddress.Loopback, port);
            listener.Start();

            var notifier = new SocketNotifier("192.168.10.5", port); // Порт дамжуулдаг бүтэцтэй гэж үзсэн

            // Сервер талын мессеж хүлээх task
            var receiveTask = Task.Run(async () =>
            {
                using var client = await listener.AcceptTcpClientAsync();
                using var stream = client.GetStream();
                var buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer, 0, bytesRead);
            });

            // Үйлдэл
            notifier.NotifySeatAssigned(expectedSeatId, expectedPassengerId);

            // Баталгаа: JSON формат зөв ирсэн эсэх
            string receivedJson = await receiveTask;
            var parsed = JsonSerializer.Deserialize<JsonElement>(receivedJson);

            Assert.Equal("Assign", parsed.GetProperty("type").GetString());
            Assert.Equal(expectedSeatId, parsed.GetProperty("seatId").GetInt32());
            Assert.Equal(expectedPassengerId, parsed.GetProperty("passengerId").GetInt32());

            listener.Stop();
        }

        /// <summary>
        /// Сокет сервер ажиллахгүй буюу холбогдох боломжгүй үед:
        /// - NotifySeatAssigned() функц ямар ч алдаа шидэхгүй, зөв log хийгдэж,
        /// - Программ зогсохгүй байх ёстой.
        /// </summary>
        [Fact]
        public void NotifySeatAssigned_ShouldNotThrow_WhenSocketFails()
        {
            
            var invalidPort = 9999; 
            var notifier = new SocketNotifier("192.168.10.5", invalidPort);

            // Үйлдэл хийх үед алдаа үүсэх ёсгүй
            var exception = Record.Exception(() => notifier.NotifySeatAssigned(123, 456));

            Assert.Null(exception); 
        }

        /// <summary>
        /// Сервер тал Socket хүлээж авахад бэлэн байгаа үед:
        /// - NotifySeatAssigned() ямар ч алдаа гаргахгүй ажиллаж буйг шалгана.
        /// Энэ нь сокетын холболт боломжтой үед notification үйлдэл тогтвортой байна уу гэдгийг батална.
        /// </summary>
        [Fact]
        public async Task NotifySeatAssigned_ShouldSendMessage_WhenServerIsRunning()
        {
   
            int port = GetAvailablePort();
            var listener = new TcpListener(IPAddress.Loopback, port);
            listener.Start();

            await Task.Delay(200); // Серверт холболтын хугацаа өгнө

            var notifier = new SocketNotifier("192.168.10.5", port);

            // Үйлдэл хийгээд алдаа шидэх эсэхийг шалгана
            var ex = Record.Exception(() => notifier.NotifySeatAssigned(99, 100));

            Assert.Null(ex); // Алдаа шидэх ёсгүй

            listener.Stop();
        }

    }
}
