using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

// Сервер рүү илгээх мессежийн объект үүсгэх
var message = new
{
    type = "Assign",      // Мессежийн төрөл
    seatId = 12,          // Суудлын дугаар
    passengerId = 104     // Зорчигчийн дугаар
};

// Объектийг JSON формат руу хөрвүүлэх
var json = JsonSerializer.Serialize(message, new JsonSerializerOptions
{
    Converters = { new JsonStringEnumConverter() }
});

// TCP клиент үүсгэж, сервертэй холбогдох (localhost:5050)
using var client = new TcpClient("localhost", 5050);
// Сүлжээний урсгал авах
var stream = client.GetStream();
// JSON өгөгдлийг UTF8 кодчиллоор хөрвүүлэх
var data = Encoding.UTF8.GetBytes(json);

// Сервер рүү өгөгдөл илгээх
await stream.WriteAsync(data, 0, data.Length);

// Серверээс хариу авах буфер үүсгэх
var buffer = new byte[1024];
int count = await stream.ReadAsync(buffer, 0, buffer.Length);
// Хариуг UTF8 кодчиллоор хөрвүүлэх
string response = Encoding.UTF8.GetString(buffer, 0, count);

// Серверээс ирсэн хариуг консол дээр хэвлэх
Console.WriteLine($"✅ Серверээс ирсэн хариу: {response}");
