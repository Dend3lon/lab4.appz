using AutoMapper;
using BusinessModels;
using DomainData.Repositories;
using DomainData.UoW;
using DomainData;
using DomainData.Models;

namespace BusinessLogic.Services
{
    public class BookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void BookRoom(BookingBusinessModel bookingBusinessModel)
        {
            var booking = _mapper.Map<Booking>(bookingBusinessModel);
            var activities = _unitOfWork.ActivityRepo.GetAll()
                .Where(a => bookingBusinessModel.ActivityIds.Contains(a.Id))
                .ToList();

            booking.Activities = activities;
            _unitOfWork.BookingRepo.Create(booking);
            _unitOfWork.Save();
        }
        public void DeleteBooking(int id)
        {
            var booking = _unitOfWork.BookingRepo.GetById(id);
            if (booking != null)
            {
                _unitOfWork.BookingRepo.Delete(id);
                _unitOfWork.Save();
            }
        }
        public List<BookingBusinessModel> GetAllBookings()
        {
            var bookings = _unitOfWork.BookingRepo.GetAll();
            return _mapper.Map<List<BookingBusinessModel>>(bookings);
        }
        public BookingBusinessModel GetBookingByRoomAndTime(int roomNumber, DateTime start, DateTime end)
        {
            var booking = _unitOfWork.BookingRepo
                .GetAll()
                .FirstOrDefault(b =>
                    b.Room.RoomNumber == roomNumber &&
                    b.StartTime < end &&
                    b.EndTime > start);

            return booking == null ? null : _mapper.Map<BookingBusinessModel>(booking);
        }
        public List<BookingBusinessModel> GetBookingByRoomId(int roomId)
        {
            var bookings = _unitOfWork.BookingRepo.GetAll()
                .Where(b => b.RoomId == roomId)
                .ToList();
            return _mapper.Map<List<BookingBusinessModel>>(bookings);
        }
    }
}
