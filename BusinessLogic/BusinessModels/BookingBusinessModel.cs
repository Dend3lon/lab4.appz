using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainData.Models;

namespace BusinessLogic.BusinessModels
{
    public class BookingBusinessModel
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string VisitorName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<int> ActivityIds { get; set; } = new();
    }
}
