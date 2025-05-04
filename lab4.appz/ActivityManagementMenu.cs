using BusinessLogic.Services;
using BusinessLogic.BusinessModels;


public class ActivityManagementMenu
{
    private readonly ActivityService _activityService;

    public ActivityManagementMenu(ActivityService activityService)
    {
        _activityService = activityService;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Управління активностями ===");
            Console.WriteLine("1. Додати активність");
            Console.WriteLine("2. Видалити активність");
            Console.WriteLine("3. Переглянути всі активності");
            Console.WriteLine("4. Назад");

            Console.Write("Оберіть дію: ");
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
                    ViewAllActivities();
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

    private void AddActivity()
    {
        Console.Write("Введіть назву активності: ");
        var name = Console.ReadLine();
        _activityService.CreateActivity(new ActivityBusinessModel { Name = name });
        Console.WriteLine("Активність додана.");
    }

    private void DeleteActivity()
    {
        Console.Write("Введіть ID активності для видалення: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            _activityService.DeleteActivity(id);
            Console.WriteLine("Активність видалена");
        }
        else
        {
            Console.WriteLine("Некоректне число.");
        }
    }

    private void ViewAllActivities()
    {
        var activities = _activityService.GetAllActivities();
        foreach (var activity in activities)
        {
            Console.WriteLine($"ID: {activity.Id}, Назва: {activity.Name}");
        }
    }
}

