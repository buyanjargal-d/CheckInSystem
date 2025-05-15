using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CheckInServer.Socket.Models;

namespace CheckInServer.Socket.Services;

public class SeatSocketService
{
    private const int Port = 5050;

    public void Start()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, Port);
        listener.Start();
        Console.WriteLine($"Socket server listening on port {Port}...");

        Task.Run(async () =>
        {
            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        });
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        Console.WriteLine("Client connected.");
        using var stream = client.GetStream();
        var buffer = new byte[1024];
        int read = await stream.ReadAsync(buffer, 0, buffer.Length);
        var json = Encoding.UTF8.GetString(buffer, 0, read);

        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };

            SocketMessage? message = JsonSerializer.Deserialize<SocketMessage>(json, options);

            if (message != null)
            {
                Console.WriteLine($"Message Type: {message.Type}, Seat: {message.SeatId}, Passenger: {message.PassengerId}");

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

        // Reply to client
        var response = Encoding.UTF8.GetBytes("ACK");
        await stream.WriteAsync(response, 0, response.Length);
    }
}
