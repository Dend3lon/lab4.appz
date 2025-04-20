using DomainData.Models;
using Microsoft.EntityFrameworkCore;

namespace DomainData
{
    public class AntiCafeRepository
    {
        private readonly AntiCafeContext _context;

        public AntiCafeRepository()
        {
            _context = new AntiCafeContext();
            _context.Database.EnsureCreated();
        }

        public void CreateRoom(int roomNumber, int capacity)
        {
            var room = new Room { RoomNumber = roomNumber, Capacity = capacity };
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void CreateActivity(string name)
        {
            if (!_context.Activities.Any(a => a.Name == name))
            {
                var activity = new Activity { Name = name };
                _context.Activities.Add(activity);
                _context.SaveChanges();
            }
        }

        public Room? FindRoomByNumber(int roomNumber)
        {
            return _context.Rooms
                .Include(r => r.Bookings)
                .ThenInclude(b => b.Activities)
                .FirstOrDefault(r => r.RoomNumber == roomNumber);
        }

        public List<Activity> GetAllActivities()
        {
            return _context.Activities.ToList();
        }

        public List<Room> GetAllRooms()
        {
            return _context.Rooms.Include(r => r.Bookings).ToList();
        }

        public void BookRoom(int roomNumber, string visitor, DateTime start, DateTime end, List<int> activityIds)
        {
            var room = _context.Rooms
                .Include(r => r.Bookings)
                .FirstOrDefault(r => r.RoomNumber == roomNumber);

            if (room == null) throw new Exception("Зал не знайдено");

            bool isBusy = room.Bookings.Any(b =>
                (start < b.EndTime && end > b.StartTime));

            if (isBusy) throw new Exception("Зал зайнятий у вибраний період");

            var activities = _context.Activities
                .Where(a => activityIds.Contains(a.Id)).ToList();

            var booking = new Booking
            {
                VisitorName = visitor,
                StartTime = start,
                EndTime = end,
                Room = room,
                Activities = activities
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

    }
}
