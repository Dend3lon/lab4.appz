using DomainData;
using System.Globalization;

namespace lab4.appz
{
    public class Menu
    {
        private readonly AntiCafeRepository _repository;

        public Menu()
        {
            _repository = new AntiCafeRepository();
        }

        public void Show()
        {
            while (true)
            {
                Console.WriteLine("\n===== Антикафе Меню =====");
                Console.WriteLine("1. Забронювати кімнату");
                Console.WriteLine("2. Створити нову кімнату");
                Console.WriteLine("3. Пошук кімнати за номером");
                Console.WriteLine("4. Додати активність");
                Console.WriteLine("5. Вийти");
                Console.Write("Виберіть опцію: ");

                var choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": BookRoom(); break;
                    case "2": CreateRoom(); break;
                    case "3": FindRoom(); break;
                    case "4": AddActivity(); break;
                    case "5": return;
                    default: Console.WriteLine("Невірний вибір. Спробуйте ще раз."); break;
                }
            }
        }

        private void CreateRoom()
        {
            Console.Write("Номер кімнати: ");
            int number = int.Parse(Console.ReadLine()!);
            Console.Write("Місткість: ");
            int capacity = int.Parse(Console.ReadLine()!);

            _repository.CreateRoom(number, capacity);
            Console.WriteLine("Кімнату створено.");
        }

        private void AddActivity()
        {
            Console.Write("Назва активності: ");
            string name = Console.ReadLine()!;
            _repository.CreateActivity(name);
            Console.WriteLine("Активність додано.");
        }

        private void FindRoom()
        {
            Console.Write("Введіть номер кімнати: ");
            int number = int.Parse(Console.ReadLine()!);
            var room = _repository.FindRoomByNumber(number);

            if (room == null)
            {
                Console.WriteLine("Кімната не знайдена.");
                return;
            }

            Console.WriteLine($"Кімната #{room.RoomNumber}, місткість: {room.Capacity}");
            Console.WriteLine("Бронювання:");

            foreach (var booking in room.Bookings)
            {
                var acts = booking.Activities.Select(a => a.Name);
                Console.WriteLine($"- {booking.VisitorName} з {booking.StartTime} до {booking.EndTime} | Активності: {string.Join(", ", acts)}");
            }
        }

        private void BookRoom()
        {
            Console.Write("Номер кімнати: ");
            int roomNumber = int.Parse(Console.ReadLine()!);
            Console.Write("Ім'я відвідувача: ");
            string visitor = Console.ReadLine()!;
            Console.Write("Час початку (yyyy-MM-dd HH:mm): ");
            Console.Write("Час початку (HH:mm): ");
            TimeSpan startTime = TimeSpan.ParseExact(Console.ReadLine()!, "hh\\:mm", CultureInfo.InvariantCulture);
            Console.Write("Час завершення (HH:mm): ");
            TimeSpan endTime = TimeSpan.ParseExact(Console.ReadLine()!, "hh\\:mm", CultureInfo.InvariantCulture);
            DateTime today = DateTime.Today;
            DateTime start = today.Add(startTime);
            DateTime end = today.Add(endTime);

            var allActs = _repository.GetAllActivities();
            Console.WriteLine("Доступні активності:");
            foreach (var a in allActs)
                Console.WriteLine($"{a.Id}. {a.Name}");

            Console.Write("Введіть ID активностей через кому: ");
            var ids = Console.ReadLine()!
                .Split(',')
                .Select(x => int.Parse(x.Trim()))
                .ToList();

            try
            {
                _repository.BookRoom(roomNumber, visitor, start, end, ids);
                Console.WriteLine("Бронювання успішне.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }
    }
}