using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoModels
{
    public class BookingDto
    {
        public int RoomNumber { get; set; }
        public string VisitorName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<int> ActivityIds { get; set; } = new List<int>();
    }
}
