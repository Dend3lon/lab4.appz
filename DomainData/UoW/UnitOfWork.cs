using DomainData.Models;
using DomainData.Repositories;

namespace DomainData.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AntiCafeContext _context;
        private IGenericRepository<Room> _roomRepo;
        private IGenericRepository<Activity> _activityRepo;
        private IGenericRepository<Booking> _bookingRepo;
        public UnitOfWork()
        {
            _context = new AntiCafeContext();
        }
        public IGenericRepository<Room> RoomRepo
        {
            get
            {
                if (_roomRepo == null)
                    _roomRepo = new GenericRepository<Room>(_context);
                return _roomRepo;
            }
        }
        public IGenericRepository<Activity> ActivityRepo
        {
            get
            {
                if (_activityRepo == null)
                    _activityRepo = new GenericRepository<Activity>(_context);
                return _activityRepo;
            }
        }
        public IGenericRepository<Booking> BookingRepo
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
