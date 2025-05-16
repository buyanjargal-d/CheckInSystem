using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CheckInServer.Socket.Models;

namespace CheckInServer.Socket.Services;

// SeatSocketService ангилал нь TCP сокет серверийг ажиллуулж, 
// клиентээс ирсэн суудлын захиалгын мэдээллийг хүлээн авч боловсруулна.

public class SeatSocketService
{
    // Серверийн сонсох портын дугаар
    private const int Port = 5050;

    // Серверийг эхлүүлэх функц
    public void Start()
    {
        // Бүх IP хаяг дээр сонсох TCPListener үүсгэнэ
        TcpListener listener = new TcpListener(IPAddress.Any, Port);
        listener.Start();
        Console.WriteLine($"Socket server listening on port {Port}...");

        // Клиент холбогдохыг тасралтгүй хүлээнэ
        Task.Run(async () =>
        {
            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        });
    }

    // Клиентээс ирсэн мэдээллийг асинхрон байдлаар боловсруулна
    private async Task HandleClientAsync(TcpClient client)
    {
        Console.WriteLine("Client connected.");
        using var stream = client.GetStream();
        var buffer = new byte[1024];
        int read = await stream.ReadAsync(buffer, 0, buffer.Length);
        var json = Encoding.UTF8.GetString(buffer, 0, read);

        try
        {
            // JSON сериализаторын тохиргоо
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            // Клиентээс ирсэн JSON мэдээллийг SocketMessage объект болгон хөрвүүлнэ
            SocketMessage? message = JsonSerializer.Deserialize<SocketMessage>(json, options);

            if (message != null)
            {
                Console.WriteLine($"Message Type: {message.Type}, Seat: {message.SeatId}, Passenger: {message.PassengerId}");

                // Захиалгын төрлөөс хамааран үйлдэл гүйцэтгэнэ
                switch (message.Type)
                {
                    //case SocketMessageType.Lock:
                    //    Console.WriteLine("Locking seat...");
                    //    break;
                    case SocketMessageType.Assign:
                        Console.WriteLine("Assigning seat...");
                        break;
                        //case SocketMessageType.Unlock:
                        //    Console.WriteLine("Unlocking seat...");
                        //    break;
                }
            }
            else
            {
                Console.WriteLine("Invalid message received.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to parse JSON: " + ex.Message);
        }

        // Клиент рүү хариу илгээнэ
        var response = Encoding.UTF8.GetBytes("ACK");
        await stream.WriteAsync(response, 0, response.Length);
    }
}
