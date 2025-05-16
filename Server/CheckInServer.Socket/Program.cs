// Socket серверийг эхлүүлэх програмын үндсэн файл
using CheckInServer.Socket.Services;

Console.WriteLine("Сокет серверийг эхлүүлж байна...");

// SeatSocketService классын шинэ объект үүсгэж байна
var server = new SeatSocketService();

// Серверийг эхлүүлэх Start() функцийг дуудаж байна
server.Start();

// Програмыг шууд хаагдахаас сэргийлж хэрэглэгчийн оролтыг хүлээж байна
Console.ReadLine();