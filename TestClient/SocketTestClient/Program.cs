using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

var message = new
{
    type = "Assign",
    seatId = 12,
    passengerId = 104
};

var json = JsonSerializer.Serialize(message, new JsonSerializerOptions
{
    Converters = { new JsonStringEnumConverter() }
});

using var client = new TcpClient("localhost", 5050);
var stream = client.GetStream();
var data = Encoding.UTF8.GetBytes(json);

await stream.WriteAsync(data, 0, data.Length);

var buffer = new byte[1024];
int count = await stream.ReadAsync(buffer, 0, buffer.Length);
string response = Encoding.UTF8.GetString(buffer, 0, count);

Console.WriteLine($"✅ Response from server: {response}");
