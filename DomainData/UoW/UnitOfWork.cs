using DomainData.Models;
using DomainData.Repositories;

namespace DomainData.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AntiCafeContext _context;
        private GenericRepository<Room> _roomRepo;
        private GenericRepository<Activity> _activityRepo;
        private GenericRepository<Booking> _bookingRepo;
        public UnitOfWork()
        {
            _context = new AntiCafeContext();
        }
        public GenericRepository<Room> RoomRepo
        {
            get
            {
                if (_roomRepo == null)
                    _roomRepo = new GenericRepository<Room>(_context);
                return _roomRepo;
            }
        }
        public GenericRepository<Activity> ActivityRepo
        {
            get
            {
                if (_activityRepo == null)
                    _activityRepo = new GenericRepository<Activity>(_context);
                return _activityRepo;
            }
        }
        public GenericRepository<Booking> BookingRepo
        {
            get
            {
                if (_bookingRepo == null)
                    _bookingRepo = new GenericRepository<Booking>(_context);
                return _bookingRepo;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
