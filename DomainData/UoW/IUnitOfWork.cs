using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainData.Repositories;
using DomainData.Models;

namespace DomainData.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        public GenericRepository<Room> RoomRepo { get; }
        public GenericRepository<Activity> ActivityRepo { get; }
        public GenericRepository<Booking> BookingRepo { get; }
        public void Save();
    }
}
