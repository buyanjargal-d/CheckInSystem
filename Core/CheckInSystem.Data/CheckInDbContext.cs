using Microsoft.EntityFrameworkCore;
using CheckInSystem.Data.Models;
using CheckInSystem.DTO.Enums;

namespace CheckInSystem.Data;

/// <summary>
/// CheckInDbContext ангилал нь Entity Framework Core ашиглан нислэгийн бүртгэлийн системийн өгөгдлийн сангийн контекстийг тодорхойлно.
/// Энэ ангилал нь нислэг (Flight), суудал (Seat), зорчигч (Passenger) зэрэг өгөгдлийн сангийн хүснэгтүүдийг удирдана.
/// </summary>
public class CheckInDbContext : DbContext
{
    /// <summary>
    /// CheckInDbContext-ийн шинэ жишээг үүсгэнэ.
    /// </summary>
    /// <param name="options">DbContext-ийн тохиргооны параметрүүд.</param>
    public CheckInDbContext(DbContextOptions<CheckInDbContext> options) : base(options) { }

    /// <summary>
    /// Нислэгийн хүснэгтэд хандах DbSet.
    /// </summary>
    public DbSet<Flight> Flights => Set<Flight>();

    /// <summary>
    /// Суудлын хүснэгтэд хандах DbSet.
    /// </summary>
    public DbSet<Seat> Seats => Set<Seat>();

    /// <summary>
    /// Зорчигчийн хүснэгтэд хандах DbSet.
    /// </summary>
    public DbSet<Passenger> Passengers { get; set; }

    /// <summary>
    /// Өгөгдлийн сангийн анхны өгөгдлийг (seed data) тохируулна.
    /// </summary>
    /// <param name="modelBuilder">ModelBuilder объект.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Нислэгийн анхны өгөгдөл
        modelBuilder.Entity<Flight>().HasData(
            new Flight
            {
                Id = 1,
                FlightNumber = "AB123",
                Status = "CheckingIn",
                DepartureTime = new DateTime(2025, 05, 15, 8, 0, 0, DateTimeKind.Utc)
            },
            new Flight
            {
                Id = 2,
                FlightNumber = "CN345",
                Status = "Scheduled",
                DepartureTime = new DateTime(2025, 05, 15, 8, 0, 0, DateTimeKind.Utc)
            }
        );

        // Зорчигчийн анхны өгөгдөл
        modelBuilder.Entity<Passenger>().HasData(
           new Passenger
           {
               PassengerId = 101,
               FullName = "John Doe",
               PassportNumber = "Y12345678",
               FlightId = 1,
               Status = PassengerStatus.Booked
           },
           new Passenger
           {
               PassengerId = 102,
               FullName = "Alice Smith",
               PassportNumber = "X98765432",
               FlightId = 1,
               Status = PassengerStatus.Booked
           },
           new Passenger
           {
               PassengerId = 103,
               FullName = "Buyana Dma",
               PassportNumber = "BB1234567",
               FlightId = 2,
               Status = PassengerStatus.Booked
           }
       );

        // Суудлын анхны өгөгдөл
        modelBuilder.Entity<Seat>().HasData(
            new Seat { SeatId = 201, SeatNumber = "12A", IsOccupied = false, FlightId = 1 },
            new Seat { SeatId = 202, SeatNumber = "12B", IsOccupied = false, FlightId = 1 },
            new Seat { SeatId = 203, SeatNumber = "12C", IsOccupied = false, FlightId = 1 },
            new Seat { SeatId = 204, SeatNumber = "12A", IsOccupied = false, FlightId = 2 },
            new Seat { SeatId = 205, SeatNumber = "12B", IsOccupied = false, FlightId = 2 },
            new Seat { SeatId = 206, SeatNumber = "12C", IsOccupied = false, FlightId = 2 }
        );
    }
}
