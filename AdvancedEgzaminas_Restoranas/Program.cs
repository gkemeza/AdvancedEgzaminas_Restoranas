using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.Services;

namespace AdvancedEgzaminas_Restoranas
{
    // - use Singleton pattern

    public class Program
    {
        static void Main(string[] args)
        {
            IDataAccess dataAccess = new DataAccess();
            Restaurant restaurant = new Restaurant(dataAccess, @"..\..\..\Data\drinks.csv");

            restaurant.Run();
        }
    }
}