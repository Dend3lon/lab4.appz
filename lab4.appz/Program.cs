using System.Text;
using BusinessLogic;
using BusinessLogic.Services;
using DomainData;
using DomainData.Models;
using DomainData.UoW;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;


        var mapper = MapperConfig.ConfigureMapper();
        var unitOfWork = new UnitOfWork();

        var roomService = new RoomService(unitOfWork, mapper);
        var activityService = new ActivityService(unitOfWork, mapper);
        var bookingService = new BookingService(unitOfWork, mapper);

        var menu = new Menu(roomService, activityService, bookingService);
        menu.Show();
    }

}