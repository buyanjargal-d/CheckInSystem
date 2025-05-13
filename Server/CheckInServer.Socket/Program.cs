using CheckInServer.Socket.Services;

Console.WriteLine("Starting Socket Server...");
var server = new SeatSocketService();
server.Start();

Console.ReadLine();