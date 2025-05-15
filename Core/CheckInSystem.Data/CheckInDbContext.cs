using Microsoft.EntityFrameworkCore;
using CheckInSystem.Data.Models;
using CheckInSystem.DTO.Enums;

namespace CheckInSystem.Data;

public class CheckInDbContext : DbContext
{
    public CheckInDbContext(DbContextOptions<CheckInDbContext> options) : base(options) {}

    public DbSet<Flight> Flights => Set<Flight>();
    public DbSet<Seat> Seats => Set<Seat>();
    public DbSet<Passenger> Passengers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
