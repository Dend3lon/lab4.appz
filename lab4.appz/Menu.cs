using BusinessLogic.Services;
using DomainData;


public class Menu
{
    private readonly RoomManagementMenu _roomMenu;
    private readonly ActivityManagementMenu _activityMenu;
    private readonly BookingManagementMenu _bookingMenu;

    public Menu(RoomService roomService, ActivityService activityService, BookingService bookingService)
    {
        _roomMenu = new RoomManagementMenu(roomService, bookingService);
        _activityMenu = new ActivityManagementMenu(activityService);
        _bookingMenu = new BookingManagementMenu(bookingService, roomService, activityService);
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Антикафе ===");
            Console.WriteLine("1. Бронювання кімнат");
            Console.WriteLine("2. Управління кімнатами");
            Console.WriteLine("3. Управління активностями");
            Console.WriteLine("4. Управління бронюваннями");
            Console.WriteLine("5. Вихід");

            Console.Write("Оберіть дію: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    _bookingMenu.BookRoom();
                    break;
                case "2":
                    _roomMenu.Show();
                    break;
                case "3":
                    _activityMenu.Show();
                    break;
                case "4":
                    _bookingMenu.Show();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }
    }
}

