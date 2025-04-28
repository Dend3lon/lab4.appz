using DomainData;


namespace lab4.appz
{
    public class Menu
    {
        public void Show()
        {
            var roomMenu = new RoomManagementMenu();
            var activityMenu = new ActivityManagementMenu();
            var bookingMenu = new BookingManagementMenu();
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
                        bookingMenu.BookRoom();
                        break;
                    case "2":
                        roomMenu.Show();
                        break;
                    case "3":
                        activityMenu.Show();
                        break;
                    case "4":
                        bookingMenu.Show();
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
}
