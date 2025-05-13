namespace CheckInServer.Socket.Models;

public enum SocketMessageType
{
    Lock,
    Assign,
    Unlock
}

public class SocketMessage
{
    public SocketMessageType Type { get; set; }
    public int SeatId { get; set; }
    public int? PassengerId { get; set; }
}

