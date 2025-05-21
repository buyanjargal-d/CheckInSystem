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
        var flightNumbers = new[] { "US123", "CN234", "FR345", "JP456", "DE567", "MN678" };
        var flights = flightNumbers.Select((num, i) => new Flight
        {
            Id = i + 1,
            FlightNumber = num,
            Status = "Scheduled",
            DepartureTime = new DateTime(2025, 05, 15, 8 + i, 0, 0, DateTimeKind.Utc)
        }).ToList();
        modelBuilder.Entity<Flight>().HasData(flights);

        var seats = new List<Seat>();
        int seatId = 1;
        for (int flightId = 1; flightId <= 6; flightId++)
        {
            for (int i = 1; i <= 20; i++)
            {
                seats.Add(new Seat
                {
                    SeatId = seatId++,
                    SeatNumber = $"S{i:D2}",
                    IsOccupied = false,
                    FlightId = flightId
                });
            }
        }
        modelBuilder.Entity<Seat>().HasData(seats);

        var names = new[]
        {
        "Alice Johnson", "Bob Smith", "Charlie Lee", "Diana King", "Ethan Hall",
        "Fiona White", "George Brown", "Hannah Scott", "Ian Davis", "Jane Miller",
        "Kevin Wilson", "Luna Thomas", "Michael Lewis", "Nora Harris", "Oliver Young",
        "Paula Green", "Quentin Hill", "Rachel Adams", "Samuel Baker", "Tina Clark",
        "Umar Turner", "Violet Moore", "William Wright", "Xenia Lopez", "Yusuf Walker",
        "Zara Price", "Aaron Murphy", "Beatrice Wood", "Caleb Reed", "Dana Collins"
    };

        var passengers = new List<Passenger>();
        int passengerId = 1;
        int nameIndex = 0;
        var passports = new[]
        {
            "AA1234567", "BB2345678", "CC3456789", "DD4567890", "EE5678901",
            "FF6789012", "GG7890123", "HH8901234", "II9012345", "JJ0123456",
            "KK1234567", "LL2345678", "MM3456789", "NN4567890", "OO5678901",
            "PP6789012", "QQ7890123", "RR8901234", "SS9012345", "TT0123456",
            "UU1234567", "VV2345678", "WW3456789", "XX4567890", "YY5678901",
            "ZZ6789012", "AB7890123", "CD8901234", "EF9012345", "GH0123456"
        };


        for (int flightId = 1; flightId <= 6; flightId++)
        {
            for (int i = 0; i < 5; i++)
            {
                passengers.Add(new Passenger
                {
                    PassengerId = passengerId,
                    FullName = names[nameIndex],
                    PassportNumber = passports[passengerId - 1],
                    FlightId = flightId,
                    Status = PassengerStatus.Booked
                });

                passengerId++;
                nameIndex++;
            }
        }


        modelBuilder.Entity<Passenger>().HasData(passengers);
    }


}
