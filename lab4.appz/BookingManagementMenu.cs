using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainData;

namespace lab4.appz
{
    public class BookingManagementMenu
    {
        private readonly BookingRepository _bookingRepository = new BookingRepository();
        private readonly ActivityRepository _activityRepository = new ActivityRepository();
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управління бронюваннями ===");
                Console.WriteLine("1. Переглянути всі бронювання");
                Console.WriteLine("2. Видалити бронювання");
                Console.WriteLine("3. Назад");

                Console.Write("Оберіть дію: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewAllBookings();
                        break;
                    case "2":
                        DeleteBooking();
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
        private void ViewAllBookings()
        {
            var bookings = _bookingRepository.GetAllBookings();
            foreach (var booking in bookings)
            {
                Console.WriteLine($"Бронювання №{booking.Id}: {booking.VisitorName} в кімнаті №{booking.Room.RoomNumber}, з {booking.StartTime} до {booking.EndTime}");
                Console.WriteLine("Активності:");
                foreach (var act in booking.Activities)
                {
                    Console.WriteLine($" - {act.Name}");
                }
                Console.WriteLine();
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
