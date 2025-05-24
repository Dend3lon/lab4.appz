using BusinessLogic.Services;
using BusinessModels;

public class RoomManagementMenu
{
    private readonly RoomService _roomService;
    private readonly BookingService _bookingService;

    public RoomManagementMenu(RoomService roomService, BookingService bookingService)
    {
        _roomService = roomService;
        _bookingService = bookingService;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Управління кімнатами ===");
            Console.WriteLine("1. Додати кімнату");
            Console.WriteLine("2. Видалити кімнату");
            Console.WriteLine("3. Переглянути всі кімнати");
            Console.WriteLine("4. Назад");

            Console.Write("Оберіть дію: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddRoom();
                    break;
                case "2":
                    DeleteRoom();
                    break;
                case "3":
                    ViewAllRoomsWithBookings();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }

            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }
    }

    private void AddRoom()
    {
        Console.Write("Введіть номер кімнати: ");
        int roomNumber = int.Parse(Console.ReadLine());

        Console.Write("Введіть місткість кімнати: ");
        if (!int.TryParse(Console.ReadLine(), out int capacity))
        {
            Console.WriteLine("Некоректна місткість.");
            return;
        }

        var roomDto = new RoomBusinessModel
        {
            RoomNumber = roomNumber,
            Capacity = capacity
        };

        _roomService.CreateRoom(roomDto);
        Console.WriteLine("Кімнату додано.");
    }

    private void DeleteRoom()
    {
        var rooms = _roomService.GetAllRooms();
        Console.Write("Введіть ID кімнати для видалення: ");
        foreach (var room in rooms)
        {
            Console.WriteLine($"{room.Id}. Номер: {room.RoomNumber}, Місткість: {room.Capacity}");
        }
        if (!int.TryParse(Console.ReadLine(), out int roomId))
        {
            Console.WriteLine("Некоректний ID.");
            return;
        }

        _roomService.DeleteRoom(roomId);
        Console.WriteLine("Кімнату видалено.");
    }

    private void ViewAllRoomsWithBookings()
    {
        var rooms = _roomService.GetAllRooms();
        foreach (var room in rooms)
        {
            Console.WriteLine($"ID: {room.Id}, Номер: {room.RoomNumber}, Місткість: {room.Capacity}");

            var bookings = _bookingService.GetBookingByRoomId(room.Id);
            if (bookings == null)
            {
                Console.WriteLine("  Бронювань немає.");
            }
            else
            {
                foreach (var booking in bookings)
                {
                    Console.WriteLine($"  [{booking.Id}] {booking.StartTime} - {booking.EndTime}");
                }
            }
        }
    }
}
