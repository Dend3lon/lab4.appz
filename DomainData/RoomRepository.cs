using DomainData.Models;

namespace DomainData
{
    public class RoomRepository
    {
        private readonly AntiCafeContext _context;

        public RoomRepository()
        {
            _context = new AntiCafeContext();
        }

        public void CreateRoom(int number, int capacity)
        {
            var room = new Room
            {
                RoomNumber = number,
                Capacity = capacity
            };
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public bool DeleteRoom(int number)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.RoomNumber == number);
            if (room == null)
                return false;

            _context.Rooms.Remove(room);
            _context.SaveChanges();
            return true;
        }

        public Room FindRoomByNumber(int number)
        {
            return _context.Rooms
                .Where(r => r.RoomNumber == number)
                .FirstOrDefault();
        }

        public List<Room> GetAllRooms()
        {
            return _context.Rooms.ToList();
        }
    }
}
