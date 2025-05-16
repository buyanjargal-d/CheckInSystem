using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

// HttpClient объектийг үүсгэж, үндсэн хаягийг тохируулна
var http = new HttpClient();
http.BaseAddress = new Uri("http://localhost:5052");
http.DefaultRequestHeaders.Add("Accept", "application/json");

// Серверийн гэрчилгээг шалгахгүйгээр зөвшөөрөх тохиргоо
HttpClientHandler handler = new HttpClientHandler();
handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

// Суудал оноох хүсэлтийн өгөгдлийг бэлтгэнэ
var assignRequest = new
{
    passengerId = 101, // зорчигчийн дугаар
    seatId = 15       // суудлын дугаар
};

Console.WriteLine("Суудал оноох хүсэлт илгээж байна...");
// Хүсэлт илгээх хаягийг хэвлэнэ
Console.WriteLine($"Хүсэлт илгээж байна: {http.BaseAddress}api/seats/assign");
// POST хүсэлтийг илгээж, хариуг авна
var response = await http.PostAsJsonAsync("api/seats/assign", assignRequest);

// Хариу амжилттай бол үр дүнг хэвлэнэ
if (response.IsSuccessStatusCode)
{
    var result = await response.Content.ReadAsStringAsync();
    Console.WriteLine("Амжилттай: " + result);
}
else
{
    // Хэрэв амжилтгүй бол статус кодыг хэвлэнэ
    Console.WriteLine("Амжилтгүй: " + response.StatusCode);
}
