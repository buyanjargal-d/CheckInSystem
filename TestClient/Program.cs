using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

var http = new HttpClient();
http.BaseAddress = new Uri("http://localhost:5052"); // Match your API port
http.DefaultRequestHeaders.Add("Accept", "application/json");

// Trust the SSL cert (only for local dev — skip in production!)
HttpClientHandler handler = new HttpClientHandler();
handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

// Assign seat example
var assignRequest = new
{
    passengerId = 101,
    seatId = 15
};

Console.WriteLine("Sending seat assignment request...");
Console.WriteLine($"Requesting: {http.BaseAddress}api/seats/assign");
var response = await http.PostAsJsonAsync("api/seats/assign", assignRequest);

if (response.IsSuccessStatusCode)
{
    var result = await response.Content.ReadAsStringAsync();
    Console.WriteLine("✅ Success: " + result);
}
else
{
    Console.WriteLine("❌ Failed: " + response.StatusCode);
}
