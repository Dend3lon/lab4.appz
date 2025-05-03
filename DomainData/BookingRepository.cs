using DomainData.Models;
using Microsoft.EntityFrameworkCore;

namespace DomainData
{
    public class BookingRepository
    {
        private readonly AntiCafeContext _context;

        public BookingRepository()
        {
            _context = new AntiCafeContext();
        }

        public void BookRoom(int roomNumber, string visitorName, DateTime start, DateTime end, List<int> activityIds)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
            if (room == null)
                throw new Exception("Кімнату не знайдено.");

            var booking = new Booking
            {
                Room = room,
                VisitorName = visitorName,
                StartTime = start,
                EndTime = end
            };

            var activities = _context.Activities.Where(a => activityIds.Contains(a.Id)).ToList();
            booking.Activities = activities;

            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public List<Booking> GetAllBookings()
        {
            return _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Activities)
                .ToList();
        }
        public Booking GetBookingByTimeRangeAndRoom(DateTime start, DateTime end, int roomId)
        {
            return _context.Bookings
                .FirstOrDefault(b => b.StartTime <= end && b.EndTime >= start && b.RoomId == roomId);
        }
        public List<Booking> GetBookingsByRoomId(int roomId)
        {
            return _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Activities)
                .Where(b => b.RoomId == roomId)
                .ToList();
        }
        public bool DeleteBooking(int id)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.Id == id);
            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            _context.SaveChanges();
            return true;
        }
    }
}
