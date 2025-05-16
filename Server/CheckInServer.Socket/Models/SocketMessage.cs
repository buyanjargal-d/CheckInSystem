namespace CheckInServer.Socket.Models;

// SocketMessageType нь сокет мессежийн төрлийг тодорхойлно.
public enum SocketMessageType
{
    Lock,    // Суудлыг түгжих үйлдэл
    Assign,  // Суудалд зорчигч оноох үйлдэл
    Unlock   // Суудлын түгжээг тайлах үйлдэл
}

// SocketMessage нь сокетээр дамжуулах мессежийн бүтэц юм.
public class SocketMessage
{
    public SocketMessageType Type { get; set; } // Мессежийн төрөл
    public int SeatId { get; set; }             // Суудлын дугаар
    public int? PassengerId { get; set; }       // Зорчигчийн дугаар (заавал биш)
}

