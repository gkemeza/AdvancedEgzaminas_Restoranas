using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.Services;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas
{
    // - use Singleton pattern

    public class Program
    {
        static void Main(string[] args)
        {
            IDataAccess dataAccess = new DataAccess();
            UserInterface userInterface = new UserInterface();
            IRestaurantService restaurant = new RestaurantService(dataAccess, userInterface, @"..\..\..\Data\drinks.csv");

            restaurant.Run();
        }
    }
}