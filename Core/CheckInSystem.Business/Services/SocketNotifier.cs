using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CheckInSystem.Business.Interfaces;

public class SocketNotifier : ISocketNotifier
{
    private const int Port = 5050;
    private const string Host = "localhost";

    public void NotifySeatAssigned(int seatId, int passengerId)
    {
        var message = new
        {
            type = "Assign",
            seatId,
            passengerId
        };
        SendMessage(message);
    }

    //public void NotifySeatLocked(int seatId)
    //{
    //    var message = new
    //    {
    //        type = "Lock",
    //        seatId,
    //        passengerId = (int?)null
    //    };
    //    SendMessage(message);
    //}

    //public void NotifySeatUnlocked(int seatId)
    //{
    //    var message = new
    //    {
    //        type = "Unlock",
    //        seatId,
    //        passengerId = (int?)null
    //    };
    //    SendMessage(message);
    //}

    private void SendMessage(object message)
    {
        var json = JsonSerializer.Serialize(message, new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        });

        try
        {
            using var client = new TcpClient(Host, Port);
            var stream = client.GetStream();
            var data = Encoding.UTF8.GetBytes(json);
            stream.Write(data, 0, data.Length);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send socket message: {ex.Message}");
        }
    }
}

