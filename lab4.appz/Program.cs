using System.Text;
using DomainData;
using DomainData.Models;
using lab4.appz;


class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        var menu = new Menu();
        menu.Show();
    }
}