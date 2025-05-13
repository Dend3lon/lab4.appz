using BusinessLogic.Services;
using BusinessLogic.BusinessModels;
using DomainData.Models;


public class BookingManagementMenu
{
    private readonly BookingService _bookingService;
    private readonly RoomService _roomService;
    private readonly ActivityService _activityService;

    public BookingManagementMenu(
        BookingService bookingService,
        RoomService roomService,
        ActivityService activityService)
    {
        _bookingService = bookingService;
        _roomService = roomService;
        _activityService = activityService;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Управління бронюваннями ===");
            Console.WriteLine("1. Видалити бронювання");
            Console.WriteLine("2. Переглянути всі бронювання");
            Console.WriteLine("3. Назад");

            Console.Write("Оберіть дію: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DeleteBooking();
                    break;
                case "2":
                    ViewAllBookings();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }

            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }
    }

    public void BookRoom()
    {
        var rooms = _roomService.GetAllRooms().ToList();
        
        Console.WriteLine("Доступні кімнати:");
        foreach (var room in rooms)
        {
            Console.WriteLine($"{room.Id}. Номер: {room.RoomNumber}, Місткість: {room.Capacity}");
        }
        
        Console.Write("Оберіть ID кімнати: ");
        if (!int.TryParse(Console.ReadLine(), out int roomId))
        {
            Console.WriteLine("Некоректне значення.");
            return;
        }
        Console.Write("Введіть ім'я відвідувача: ");
        string visitorName = Console.ReadLine();
        DateTime startTime, endTime;
        BookingBusinessModel booking;
        do
        {
            Console.Write("Початок (yyyy-MM-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out startTime))
            {
                Console.WriteLine("Невірний формат дати.");
                return;
            }

            Console.Write("Кінець (yyyy-MM-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out endTime))
            {
                Console.WriteLine("Невірний формат дати.");
                return;
            }
            booking = _bookingService.GetBookingByRoomAndTime(roomId, startTime, endTime);
            if (booking != null)
            {
                Console.WriteLine("Ця кімната вже заброньована на цей час. Спробуйте ще раз.");
            }
        } while (booking != null); 
        Console.WriteLine("Доступні активності:");
        var activities = _activityService.GetAllActivities().ToList();
        foreach (var activity in activities)
        {
            Console.WriteLine($"{activity.Id}. {activity.Name}");
        }

        Console.Write("Введіть ID активностей через кому (наприклад 1,2): ");
        var input = Console.ReadLine();
        var activityIds = input.Split(',')
            .Select(s => int.TryParse(s.Trim(), out int id) ? id : (int?)null)
            .Where(id => id.HasValue)
            .Select(id => id.Value)
            .ToList();

        _bookingService.BookRoom(roomId, visitorName, startTime, endTime, activityIds);

        Console.WriteLine("Бронювання успішне.");
    }

    private void DeleteBooking()
    {
        Console.Write("Введіть ID бронювання для видалення: ");
        if (!int.TryParse(Console.ReadLine(), out int bookingId))
        {
            Console.WriteLine("Некоректне значення.");
            return;
        }

        _bookingService.DeleteBooking(bookingId);
        Console.WriteLine("Бронювання видалено.");
    }

    private void ViewAllBookings()
    {
        var bookings = _bookingService.GetAllBookings();
        foreach (var booking in bookings)
        {
            Console.WriteLine($"ID: {booking.Id},Забронював: {booking.VisitorName} Кімната: {booking.RoomId}, {booking.StartTime} - {booking.EndTime},");
            //Console.WriteLine("  Активності: " + string.Join(", ", booking.Activities.Select(a => a.Name)));
        }
    }
}