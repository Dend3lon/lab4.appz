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
        public IGenericRepository<Room> RoomRepo { get; }
        public IGenericRepository<Activity> ActivityRepo { get; }
        public IGenericRepository<Booking> BookingRepo { get; }
        public void Save();
    }
}
