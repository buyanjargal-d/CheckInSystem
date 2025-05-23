using Xunit;
using Moq;
using CheckInSystem.Business.Services;
using CheckInSystem.Data.Interfaces;
using CheckInSystem.Business.Interfaces;
using CheckInSystem.DTO;
using CheckInSystem.DTO.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace CheckInSystem.Tests
{
    /// <summary>
    /// SeatAssignmentService класст зориулсан unit тестүүдийг агуулсан тест класс.
    /// </summary>
    public class SeatAssignmentServiceTests
    {
        /// <summary>
        /// Амжилттай суудал оноох тохиолдол:
        /// Хэрвээ суудал сул бөгөөд зорчигч хүчинтэй бол суудал амжилттай оноогдох ёстой.
        /// </summary>
        [Fact]
        public async Task AssignSeatAsync_ShouldReturnTrue_WhenSeatIsAvailableAndPassengerIsValid()
        {
            // Dummy өгөгдлүүд: суудлын ID, зорчигчийн ID, нислэгийн ID
            var seatId = 1;
            var passengerId = 100;
            var flightId = 10;

            // Mock repository ба notifier интерфэйсүүд
            var mockSeatRepo = new Mock<ISeatRepository>();
            var mockPassengerRepo = new Mock<IPassengerRepository>();
            var mockSocketNotifier = new Mock<ISocketNotifier>();
            var mockSeatNotifier = new Mock<ISeatNotifier>();
            var mockFlightNotifier = new Mock<IFlightNotifier>();

            // Суудал сул байгаа гэж буцаана
            mockSeatRepo.Setup(r => r.IsAvailable(seatId)).Returns(true);

            // Зорчигч байгаа бөгөөд зөв нислэгтэй холбоотой
            mockPassengerRepo.Setup(r => r.GetById(passengerId)).Returns(new PassengerDto
            {
                Id = passengerId,
                FlightId = flightId
            });

            // Оноолгохоос өмнө: 2 сул суудал
            // Оноолсны дараа: зорчигчид суудал оноогдсон
            mockSeatRepo.SetupSequence(r => r.GetAllSeatsByFlightId(flightId))
                .Returns(new List<SeatDto>
                {
                    new SeatDto { SeatId = 2, PassengerId = null },
                    new SeatDto { SeatId = 3, PassengerId = null }
                })
                .Returns(new List<SeatDto>
                {
                    new SeatDto { SeatId = seatId, PassengerId = passengerId }
                });

            // Оноолт амжилттай бол true буцаана
            mockSeatRepo.Setup(r => r.AssignSeat(passengerId, seatId)).Returns(true);

            // Үйлчилгээг бүтээж, mock-уудыг оруулж өгнө
            var service = new SeatAssignmentService(
                mockSeatRepo.Object,
                mockPassengerRepo.Object,
                mockSocketNotifier.Object,
                mockSeatNotifier.Object,
                mockFlightNotifier.Object
            );

            // Act - Үйлдэл гүйцэтгэнэ
            var result = await service.AssignSeatAsync(passengerId, seatId);

            // Assert - Үр дүнг шалгана
            Assert.True(result);

            // Зөв notifier-ууд дуудсан эсэхийг шалгана
            mockSeatNotifier.Verify(n => n.NotifySeatAssignedAsync(It.IsAny<SeatDto>()), Times.Once);
            mockSocketNotifier.Verify(n => n.NotifySeatAssigned(seatId, passengerId), Times.Once);
        }

        /// <summary>
        /// Суудал сул биш бол суудал оноох ёсгүй
        /// </summary>
        [Fact]
        public async Task AssignSeatAsync_ShouldReturnFalse_WhenSeatIsNotAvailable()
        {
            // Arrange
            var seatId = 1;
            var passengerId = 100;

            var mockSeatRepo = new Mock<ISeatRepository>();
            mockSeatRepo.Setup(r => r.IsAvailable(seatId)).Returns(false); // Суудал аль хэдийн эзлэгдсэн

            var service = new SeatAssignmentService(
                mockSeatRepo.Object,
                new Mock<IPassengerRepository>().Object,
                new Mock<ISocketNotifier>().Object,
                new Mock<ISeatNotifier>().Object,
                new Mock<IFlightNotifier>().Object
            );

            // Act
            var result = await service.AssignSeatAsync(passengerId, seatId);

            // Assert
            Assert.False(result); // Оноох ёсгүй
        }

        /// <summary>
        /// Зорчигч мэдээлэл олдохгүй үед суудал оноох ёсгүй
        /// </summary>
        [Fact]
        public async Task AssignSeatAsync_ShouldReturnFalse_WhenPassengerNotFound()
        {
            var seatId = 1;
            var passengerId = 999;

            var mockSeatRepo = new Mock<ISeatRepository>();
            mockSeatRepo.Setup(r => r.IsAvailable(seatId)).Returns(true);

            var mockPassengerRepo = new Mock<IPassengerRepository>();
            mockPassengerRepo.Setup(r => r.GetById(passengerId)).Returns((PassengerDto?)null); // Зорчигч олдохгүй

            var service = new SeatAssignmentService(
                mockSeatRepo.Object,
                mockPassengerRepo.Object,
                new Mock<ISocketNotifier>().Object,
                new Mock<ISeatNotifier>().Object,
                new Mock<IFlightNotifier>().Object
            );

            var result = await service.AssignSeatAsync(passengerId, seatId);

            Assert.False(result);
        }

        /// <summary>
        /// Зорчигчид аль хэдийн суудал оноогдсон бол дахин оноох ёсгүй
        /// </summary>
        [Fact]
        public async Task AssignSeatAsync_ShouldReturnFalse_WhenPassengerAlreadyHasSeat()
        {
            var seatId = 1;
            var passengerId = 100;
            var flightId = 10;

            var mockSeatRepo = new Mock<ISeatRepository>();
            var mockPassengerRepo = new Mock<IPassengerRepository>();

            mockSeatRepo.Setup(r => r.IsAvailable(seatId)).Returns(true);

            mockPassengerRepo.Setup(r => r.GetById(passengerId)).Returns(new PassengerDto
            {
                Id = passengerId,
                FlightId = flightId
            });

            // Зорчигчид аль хэдийн суудал оноосон
            mockSeatRepo.Setup(r => r.GetAllSeatsByFlightId(flightId)).Returns(new List<SeatDto>
            {
                new SeatDto { SeatId = 5, PassengerId = passengerId }
            });

            var service = new SeatAssignmentService(
                mockSeatRepo.Object,
                mockPassengerRepo.Object,
                new Mock<ISocketNotifier>().Object,
                new Mock<ISeatNotifier>().Object,
                new Mock<IFlightNotifier>().Object
            );

            var result = await service.AssignSeatAsync(passengerId, seatId);

            Assert.False(result);
        }

        /// <summary>
        /// AssignSeat() method амжилтгүй болвол false буцаана
        /// </summary>
        [Fact]
        public async Task AssignSeatAsync_ShouldReturnFalse_WhenAssignSeatFails()
        {
            var seatId = 1;
            var passengerId = 100;
            var flightId = 10;

            var mockSeatRepo = new Mock<ISeatRepository>();
            var mockPassengerRepo = new Mock<IPassengerRepository>();

            mockSeatRepo.Setup(r => r.IsAvailable(seatId)).Returns(true);

            mockPassengerRepo.Setup(r => r.GetById(passengerId)).Returns(new PassengerDto
            {
                Id = passengerId,
                FlightId = flightId
            });

            // Оноолтоос өмнө: суудал сул
            mockSeatRepo.Setup(r => r.GetAllSeatsByFlightId(flightId)).Returns(new List<SeatDto>
            {
                new SeatDto { SeatId = 1, PassengerId = null }
            });

            // Оноолт амжилтгүй
            mockSeatRepo.Setup(r => r.AssignSeat(passengerId, seatId)).Returns(false);

            var service = new SeatAssignmentService(
                mockSeatRepo.Object,
                mockPassengerRepo.Object,
                new Mock<ISocketNotifier>().Object,
                new Mock<ISeatNotifier>().Object,
                new Mock<IFlightNotifier>().Object
            );

            var result = await service.AssignSeatAsync(passengerId, seatId);

            Assert.False(result);
        }

        /// <summary>
        /// Нислэгийн ID-аар бүх сул суудлыг авах
        /// </summary>
        [Fact]
        public void GetAvailableSeats_ShouldReturnListOfAvailableSeats()
        {
            var flightId = 10;

            var mockSeatRepo = new Mock<ISeatRepository>();
            mockSeatRepo.Setup(r => r.GetAvailableSeats(flightId)).Returns(new List<SeatDto>
            {
                new SeatDto { SeatId = 1, IsOccupied = true },
                new SeatDto { SeatId = 2, IsOccupied = true }
            });

            var service = new SeatAssignmentService(
                mockSeatRepo.Object,
                new Mock<IPassengerRepository>().Object,
                new Mock<ISocketNotifier>().Object,
                new Mock<ISeatNotifier>().Object,
                new Mock<IFlightNotifier>().Object
            );

            var result = service.GetAvailableSeats(flightId);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        /// <summary>
        /// Бүх суудлыг нислэгийн ID-аар авах
        /// </summary>
        [Fact]
        public void GetAllSeats_ShouldReturnAllSeatsForFlight()
        {
            var flightId = 20;

            var mockSeatRepo = new Mock<ISeatRepository>();
            mockSeatRepo.Setup(r => r.GetAllSeatsByFlightId(flightId)).Returns(new List<SeatDto>
            {
                new SeatDto { SeatId = 1 },
                new SeatDto { SeatId = 2 },
                new SeatDto { SeatId = 3 }
            });

            var service = new SeatAssignmentService(
                mockSeatRepo.Object,
                new Mock<IPassengerRepository>().Object,
                new Mock<ISocketNotifier>().Object,
                new Mock<ISeatNotifier>().Object,
                new Mock<IFlightNotifier>().Object
            );

            var result = service.GetAllSeats(flightId);

            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
        }

        /// <summary>
        /// Зорчигчийн ID-аар түүний суудлыг авах
        /// </summary>
        [Fact]
        public void GetSeatByPassenger_ShouldReturnSeat_WhenExists()
        {
            var passengerId = 101;

            var mockSeatRepo = new Mock<ISeatRepository>();
            mockSeatRepo.Setup(r => r.GetSeatByPassengerId(passengerId)).Returns(
                new SeatDto { SeatId = 5, PassengerId = passengerId });

            var service = new SeatAssignmentService(
                mockSeatRepo.Object,
                new Mock<IPassengerRepository>().Object,
                new Mock<ISocketNotifier>().Object,
                new Mock<ISeatNotifier>().Object,
                new Mock<IFlightNotifier>().Object
            );

            var seat = service.GetSeatByPassenger(passengerId);

            Assert.NotNull(seat);
            Assert.Equal(passengerId, seat?.PassengerId);
        }
    }
}
