using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CheckInSystem.Business.Interfaces;

/// <summary>
/// Сокет мэдэгдэл илгээгчийн хэрэгжилт.
/// Энэ класс нь суудал оноох зэрэг үйлдлүүдийг сокет ашиглан сервер рүү илгээдэг.
/// </summary>
public class SocketNotifier : ISocketNotifier
{
    // Сокет холболтын портын дугаар
    private const int Port = 5050;
    // Сокет холбогдох хостын нэр
    private const string Host = "localhost";

    /// <summary>
    /// Суудал оноогдсон тухай мэдэгдэл илгээнэ.
    /// </summary>
    /// <param name="seatId">Суудлын дугаар</param>
    /// <param name="passengerId">Зорчигчийн дугаар</param>
    public void NotifySeatAssigned(int seatId, int passengerId)
    {
        // Илгээх мэдэгдлийн объект үүсгэнэ
        var message = new
        {
            type = "Assign",
            seatId,
            passengerId
        };
        // Мэдэгдлийг сокетоор илгээнэ
        SendMessage(message);
    }

    //private void NotifySeatLocked(int seatId)
    //{
    //    // Суудал түгжигдсэн тухай мэдэгдэл илгээнэ
    //    var message = new
    //    {
    //        type = "Lock",
    //        seatId,
    //        passengerId = (int?)null
    //    };
    //    SendMessage(message);
    //}

    //private void NotifySeatUnlocked(int seatId)
    //{
    //    // Суудал түгжээ тайлагдсан тухай мэдэгдэл илгээнэ
    //    var message = new
    //    {
    //        type = "Unlock",
    //        seatId,
    //        passengerId = (int?)null
    //    };
    //    SendMessage(message);
    //}

    /// <summary>
    /// Объектийг JSON болгон хөрвүүлж, сокет ашиглан сервер рүү илгээнэ.
    /// </summary>
    /// <param name="message">Илгээх объект</param>
    private void SendMessage(object message)
    {
        // Объектийг JSON формат руу хөрвүүлнэ
        var json = JsonSerializer.Serialize(message, new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        });

        try
        {
            // TCP клиент үүсгэж сервер рүү холбогдоно
            using var client = new TcpClient(Host, Port);
            var stream = client.GetStream();
            // JSON өгөгдлийг UTF8 кодчиллоор хөрвүүлнэ
            var data = Encoding.UTF8.GetBytes(json);
            // Өгөгдлийг сокет руу бичнэ
            stream.Write(data, 0, data.Length);
        }
        catch (Exception ex)
        {
            // Алдаа гарвал консолд хэвлэнэ
            Console.WriteLine($"Сокет мэдэгдэл илгээхэд алдаа гарлаа: {ex.Message}");
        }
    }
}

