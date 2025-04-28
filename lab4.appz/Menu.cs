using DomainData;


namespace lab4.appz
{
    public class Menu
    {
        private readonly RoomRepository _roomRepository;
        private readonly ActivityRepository _activityRepository;
        private readonly BookingRepository _bookingRepository;

        public Menu()
        {
            _roomRepository = new RoomRepository();
            _activityRepository = new ActivityRepository();
            _bookingRepository = new BookingRepository();
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("\n=== Головне меню ===");
                Console.WriteLine("1. Бронювання кімнати");
                Console.WriteLine("2. Управління кімнатами");
                Console.WriteLine("3. Управління активностями");
                Console.WriteLine("4. Управління бронюваннями");
                Console.WriteLine("0. Вихід");
                Console.Write("Оберіть опцію: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BookRoom();
                        break;
                    case "2":
                        ManageRooms();
                        break;
                    case "3":
                        ManageActivities();
                        break;
                    case "4":
                        ManageBookings();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        break;
                }
            }
        }

        private void BookRoom()
        {
            Console.Write("Номер кімнати: ");
            int roomNumber = int.Parse(Console.ReadLine());

            Console.Write("Ім'я відвідувача: ");
            string visitor = Console.ReadLine();

            Console.Write("Дата початку (yyyy-MM-dd HH:mm): ");
            DateTime start = DateTime.Parse(Console.ReadLine());

            Console.Write("Дата закінчення (yyyy-MM-dd HH:mm): ");
            DateTime end = DateTime.Parse(Console.ReadLine());

            var activities = _activityRepository.GetAllActivities();
            Console.WriteLine("Список активностей:");
            foreach (var activity in activities)
            {
                Console.WriteLine($"{activity.Id}. {activity.Name}");
            }

            Console.Write("Введіть ID активностей через кому: ");
            var ids = Console.ReadLine()
                .Split(',')
                .Select(id => int.Parse(id.Trim()))
                .ToList();

            try
            {
                _bookingRepository.BookRoom(roomNumber, visitor, start, end, ids);
                Console.WriteLine("Бронювання успішне!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        private void ManageRooms()
        {
            while (true)
            {
                Console.WriteLine("\n--- Управління кімнатами ---");
                Console.WriteLine("1. Додати кімнату");
                Console.WriteLine("2. Видалити кімнату");
                Console.WriteLine("3. Переглянути кімнату за номером");
                Console.WriteLine("4. Переглянути всі кімнати");
                Console.WriteLine("0. Назад");

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
                        FindRoom();
                        break;
                    case "4":
                        ViewRooms();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        private void ManageActivities()
        {
            while (true)
            {
                Console.WriteLine("\n--- Управління активностями ---");
                Console.WriteLine("1. Додати активність");
                Console.WriteLine("2. Видалити активність");
                Console.WriteLine("3. Переглянути всі активності");
                Console.WriteLine("0. Назад");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddActivity();
                        break;
                    case "2":
                        DeleteActivity();
                        break;
                    case "3":
                        ViewActivities();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        private void ManageBookings()
        {
            while (true)
            {
                Console.WriteLine("\n--- Управління бронюваннями ---");
                Console.WriteLine("1. Переглянути всі бронювання");
                Console.WriteLine("2. Видалити бронювання");
                Console.WriteLine("0. Назад");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewBookings();
                        break;
                    case "2":
                        DeleteBooking();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        private void AddRoom()
        {
            Console.Write("Номер кімнати: ");
            int number = int.Parse(Console.ReadLine());

            Console.Write("Кількість місць: ");
            int capacity = int.Parse(Console.ReadLine());

            _roomRepository.CreateRoom(number, capacity);
            Console.WriteLine("Кімната додана!");
        }

        private void DeleteRoom()
        {
            Console.Write("Введіть номер кімнати для видалення: ");
            int number = int.Parse(Console.ReadLine());

            if (_roomRepository.DeleteRoom(number))
                Console.WriteLine("Кімната видалена.");
            else
                Console.WriteLine("Кімнату не знайдено.");
        }

        private void FindRoom()
        {
            Console.Write("Введіть номер кімнати: ");
            int number = int.Parse(Console.ReadLine());

            var room = _roomRepository.FindRoomByNumber(number);
            if (room == null)
            {
                Console.WriteLine("Кімнату не знайдено.");
                return;
            }

            Console.WriteLine($"Кімната №{room.RoomNumber}, Місць: {room.Capacity}");
            if (room.Bookings.Any())
            {
                Console.WriteLine("Бронювання:");
                foreach (var booking in room.Bookings)
                {
                    Console.WriteLine($"- {booking.VisitorName}: {booking.StartTime} - {booking.EndTime}");
                }
            }
            else
            {
                Console.WriteLine("Бронювань немає.");
            }
        }

        private void ViewRooms()
        {
            var rooms = _roomRepository.GetAllRooms();
            foreach (var room in rooms)
            {
                Console.WriteLine($"Кімната №{room.RoomNumber}, Місць: {room.Capacity}");
            }
        }

        private void AddActivity()
        {
            Console.Write("Назва активності: ");
            string name = Console.ReadLine();

            _activityRepository.CreateActivity(name);
            Console.WriteLine("Активність додана!");
        }

        private void DeleteActivity()
        {
            Console.Write("Введіть ID активності для видалення: ");
            int id = int.Parse(Console.ReadLine());

            if (_activityRepository.DeleteActivity(id))
                Console.WriteLine("Активність видалена.");
            else
                Console.WriteLine("Активність не знайдено.");
        }

        private void ViewActivities()
        {
            var activities = _activityRepository.GetAllActivities();
            foreach (var activity in activities)
            {
                Console.WriteLine($"ID: {activity.Id}, Назва: {activity.Name}");
            }
        }

        private void ViewBookings()
        {
            var bookings = _bookingRepository.GetAllBookings();
            foreach (var booking in bookings)
            {
                Console.WriteLine($"ID: {booking.Id}, Відвідувач: {booking.VisitorName}, Зал: {booking.Room.RoomNumber}, {booking.StartTime} - {booking.EndTime}");
            }
        }

        private void DeleteBooking()
        {
            Console.Write("Введіть ID бронювання для видалення: ");
            int id = int.Parse(Console.ReadLine());

            if (_bookingRepository.DeleteBooking(id))
                Console.WriteLine("Бронювання видалено.");
            else
                Console.WriteLine("Бронювання не знайдено.");
        }
    }
}
