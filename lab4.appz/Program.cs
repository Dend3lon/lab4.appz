using System.Text;
using BusinessLogic.Services;
using DomainData;
using DomainData.UoW;
using Microsoft.Extensions.DependencyInjection;
using AutoMapperProfiles;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        
        var services = new ServiceCollection();

        services.AddDbContext<AntiCafeContext>();
        services.AddAutoMapper(typeof(AutoMapperProfile));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<RoomService>();
        services.AddScoped<ActivityService>();
        services.AddScoped<BookingService>();
        services.AddTransient<Menu>();

        var serviceProvider = services.BuildServiceProvider();
        var menu = serviceProvider.GetRequiredService<Menu>();
        menu.Show();
    }
}