using System;
using System.Text;
using BusinessLogic;
using BusinessLogic.Services;
using DomainData;
using DomainData.Models;
using DomainData.UoW;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        var context = new AntiCafeContext();
        var services = new ServiceCollection();

        services.AddDbContext<AntiCafeContext>();
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
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