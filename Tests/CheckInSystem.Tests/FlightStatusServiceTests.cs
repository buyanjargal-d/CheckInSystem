using Xunit;
using Moq;
using CheckInSystem.Business.Services;
using CheckInSystem.Business.Interfaces;
using CheckInSystem.Data.Interfaces;

namespace CheckInSystem.Tests
{
    /// <summary>
    /// FlightStatusService классын нэгж тестүүд.
    /// Нислэгийн төлөв шинэчлэгдэх үед өгөгдлийн сантай ажиллаж буй repository болон
    /// мэдэгдэл илгээх notifier хоёрын аль аль нь зөв ажиллаж байгааг шалгах болно
    /// </summary>
    public class FlightStatusServiceTests
    {
        /// <summary>
        /// UpdateStatus() дуудагдах үед:
        /// - Нислэгийн төлөвийг өгөгдлийн санд шинэчлэх ёстой (UpdateStatus)
        /// - Шинэ төлвийг бусад системд мэдэгдэх ёстой (NotifyFlightStatusAsync)
        /// гэдгийг шалгах.
        /// </summary>
        [Fact]
        public void UpdateStatus_ShouldCallRepoAndNotify()
        {
            // Тохиргоо
            var flightId = 1;
            var newStatus = "Boarding";

            var mockRepo = new Mock<IFlightRepository>();
            var mockNotifier = new Mock<IFlightNotifier>();

            var service = new FlightStatusService(mockRepo.Object, mockNotifier.Object);

            // Үйлдэл
            service.UpdateStatus(flightId, newStatus);

            // Баталгаа
            mockRepo.Verify(r => r.UpdateStatus(flightId, newStatus), Times.Once);
            mockNotifier.Verify(n => n.NotifyFlightStatusAsync(flightId, newStatus), Times.Once);
        }

        /// <summary>
        /// Нислэгийн шинэ төлвийг мэдэгддэг NotifyFlightStatusAsync функц нэг удаа дуудагдаж байгаа эсэхийг шалгана.
        /// Зөвхөн мэдэгдлийн талд төвлөрсөн тест.
        /// </summary>
        [Fact]
        public void UpdateStatus_ShouldNotifyFlightStatusChange()
        {
            // Тохиргоо
            var flightId = 5;
            var newStatus = "Boarding";

            var mockRepo = new Mock<IFlightRepository>();
            var mockNotifier = new Mock<IFlightNotifier>();

            var service = new FlightStatusService(mockRepo.Object, mockNotifier.Object);

            // Үйлдэл
            service.UpdateStatus(flightId, newStatus);

            // Баталгаа
            mockNotifier.Verify(n => n.NotifyFlightStatusAsync(flightId, newStatus), Times.Once);
        }

        /// <summary>
        /// Repository-гийн UpdateStatus() функц зөв flightId ба status-тайгаар нэг удаа дуудагдаж байгаа эсэхийг шалгана.
        /// </summary>
        [Fact]
        public void UpdateStatus_ShouldUpdateFlightInRepository()
        {
            // Тохиргоо
            var flightId = 42;
            var status = "Cancelled";

            var mockRepo = new Mock<IFlightRepository>();
            var mockNotifier = new Mock<IFlightNotifier>();

            var service = new FlightStatusService(mockRepo.Object, mockNotifier.Object);

            // Үйлдэл
            service.UpdateStatus(flightId, status);

            // Баталгаа
            mockRepo.Verify(r => r.UpdateStatus(It.Is<int>(id => id == flightId), It.Is<string>(s => s == status)), Times.Once);
        }
    }
}
