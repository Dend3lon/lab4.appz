using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainData;
using DomainData.Models;

namespace lab4.appz
{
    public class RoomManagementMenu
    {
        private readonly RoomRepository _roomRepository = new RoomRepository();
        private readonly BookingRepository _bookingRepository = new BookingRepository();
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Управління кімнатами ===");
                Console.WriteLine("1. Додати кімнату");
                Console.WriteLine("2. Видалити кімнату");
                Console.WriteLine("3. Переглянути всі кімнати");
                Console.WriteLine("4. Пошук кімнати за номером");
                Console.WriteLine("5. Назад");

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
                        ViewAllRooms();
                        break;
                    case "4":
                        FindRoom();
                        break;
                    case "5":
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
            int number = int.Parse(Console.ReadLine());
            Console.Write("Введіть місткість кімнати: ");
            int capacity = int.Parse(Console.ReadLine());
            _roomRepository.CreateRoom(number, capacity);
            Console.WriteLine("Кімната додана.");
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

        private void ViewAllRooms()
        {
            var rooms = _roomRepository.GetAllRooms();
            foreach (var room in rooms)
            {
                Console.WriteLine($"Кімната №{room.RoomNumber}, Місткість: {room.Capacity}");
            }
        }

        private void FindRoom()
        {
            Console.Write("Введіть номер кімнати для пошуку: ");
            int number = int.Parse(Console.ReadLine());
            var room = _roomRepository.FindRoomByNumber(number);
            if (room != null)
            {
                Console.WriteLine($"Кімната №{room.RoomNumber}, Місткість: {room.Capacity}");
                var bookings = _bookingRepository.GetBookingsByRoomId(number); 
                Console.WriteLine("Бронювання в цій кімнаті:");
                foreach (var booking in bookings)
                {
                    Console.WriteLine($"Бронювання №{booking.Id}: {booking.VisitorName}, з {booking.StartTime} до {booking.EndTime}");
                }
            }
            else
            {
                Console.WriteLine("Кімнату не знайдено.");
            }
        }
    }
}
