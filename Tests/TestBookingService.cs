using DomainData.Repositories;
using DomainData.UoW;
using DomainData.Models;
using BusinessLogic.Services;
using Moq;
using AutoMapper;
using BusinessLogic;
using Xunit;
using BusinessModels;
using AutoMapperProfiles;

namespace Tests
{
    public class TestBookingService
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IGenericRepository<Booking>> _bookingRepositoryMock;
        private readonly Mock<IGenericRepository<Room>> _roomRepositoryMock;
        private readonly Mock<IGenericRepository<Activity>> _activityRepositoryMock;
        private readonly BookingService _bookingService;
        public TestBookingService()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _bookingRepositoryMock = new Mock<IGenericRepository<Booking>>();
            _roomRepositoryMock = new Mock<IGenericRepository<Room>>();
            _activityRepositoryMock = new Mock<IGenericRepository<Activity>>();

            _unitOfWorkMock.Setup(x => x.BookingRepo).Returns(_bookingRepositoryMock.Object);
            _unitOfWorkMock.Setup(x => x.RoomRepo).Returns(_roomRepositoryMock.Object);
            _unitOfWorkMock.Setup(x => x.ActivityRepo).Returns(_activityRepositoryMock.Object);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(AutoMapperProfile).Assembly);
            });
            var mapper = config.CreateMapper();


            _bookingService = new BookingService(_unitOfWorkMock.Object, mapper);
        }

        [Fact]
        public void TestBookingRoom()
        {
            var bookingBusinessModel = new BookingBusinessModel
            {
                RoomNumber = 1,
                VisitorName = "Test",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2),
                ActivityIds = new List<int> { 1 }
            };

            var testRoom = new Room { Id = 1, RoomNumber = 1, Capacity = 10 };
            var testActivity = new Activity { Id = 1, Name = "TestActivity" };

            _activityRepositoryMock
                .Setup(a => a.GetAll())
                .Returns(new List<Activity> { testActivity }.AsQueryable);

            _bookingService.BookRoom(bookingBusinessModel); 

            _bookingRepositoryMock.Verify(x => x.Create(It.Is<Booking>(b =>
                b.RoomId == testRoom.Id &&
                b.VisitorName == bookingBusinessModel.VisitorName &&
                b.Activities.Any(a => a.Id == testActivity.Id)
            )));
            _unitOfWorkMock.Verify(x => x.Save());
        }

        [Fact]
        public void TestDeleteBooking()
        {
            var booking = new Booking { Id = 1 };
            _bookingRepositoryMock.Setup(r => r.GetById(booking.Id)).Returns(booking);

            _bookingService.DeleteBooking(booking.Id);

            _bookingRepositoryMock.Verify(x => x.Delete(booking.Id));
            _unitOfWorkMock.Verify(x => x.Save());
        }

        [Fact]
        public void TestGetBookingByRoomAndTime()
        {
            var roomNumber = 101;
            var start = new DateTime(2024, 1, 1, 10, 0, 0);
            var end = new DateTime(2024, 1, 1, 12, 0, 0);

            var testBooking = new Booking
            {
                Id = 1,
                Room = new Room { RoomNumber = roomNumber },
                StartTime = new DateTime(2024, 1, 1, 11, 0, 0),
                EndTime = new DateTime(2024, 1, 1, 13, 0, 0)
            };

            _unitOfWorkMock
                .Setup(u => u.BookingRepo.GetAll())
                .Returns(new List<Booking> { testBooking }.AsQueryable);

            var result = _bookingService.GetBookingByRoomAndTime(roomNumber, start, end);

            Xunit.Assert.Equal(testBooking.Id, result.Id);
        }

        [Fact]
        public void TestGetBookingByRoomId()
        {
            var roomId = 1;
            var testBooking = new Booking { Id = 1, RoomId = roomId };
            _unitOfWorkMock
                .Setup(u => u.BookingRepo.GetAll())
                .Returns(new List<Booking> { testBooking }.AsQueryable);
            var result = _bookingService.GetBookingByRoomId(roomId);

            Xunit.Assert.Equal(testBooking.Id, result[0].Id);
        }
    }
}
