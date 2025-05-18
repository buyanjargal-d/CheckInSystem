using CheckInSystem.DTO.Enums;

namespace CheckInSystem.Data.Models
{
    /// <summary>
    /// Passenger нь зорчигчийн мэдээллийг хадгална.
    /// </summary>
    public class Passenger
    {
        /// <summary>
        /// Зорчигчийн давтагдашгүй дугаар.
        /// </summary>
        public int PassengerId { get; set; }

        /// <summary>
        /// Зорчигчийн бүтэн нэр.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Зорчигчийн паспортын дугаар.
        /// </summary>
        public string PassportNumber { get; set; } = string.Empty;

        /// <summary>
        /// Зорчигчийн харьяалагдах нислэгийн дугаар (ID).
        /// </summary>
        public int FlightId { get; set; }

        /// <summary>
        /// Зорчигчийн төлөв (жишээ нь: Booked, CheckedIn, Boarded, Cancelled).
        /// </summary>
        public PassengerStatus Status { get; set; }

        /// <summary>
        /// Зорчигчийн харьяалагдах нислэгийн мэдээлэл.
        /// </summary>
        public Flight Flight { get; set; } = null!;

        ///// <summary>
        ///// Зорчигчид хуваарилагдсан суудлуудын жагсаалт.
        ///// </summary>
        //public ICollection<Seat>? Seats { get; set; }
    }
}
