using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Profiles;

namespace BusinessLogic
{
    public static class MapperConfig
    {
        public static IMapper ConfigureMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BookingProfile>();
                cfg.AddProfile<RoomProfile>();
                cfg.AddProfile<ActivityProfile>();
            });

            return config.CreateMapper();
        }
    }
}
