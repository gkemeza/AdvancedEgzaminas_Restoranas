using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas.Services
{
    // to not write code in program.cs
    public class RestaurantService : IRestaurantService
    {
        private readonly IDataAccess _dataAccess;
        private readonly UserInterface _userInterface;
        private readonly string _filePath;

        public RestaurantService(IDataAccess dataAccess, UserInterface userInterface, string filePath)
        {
            _dataAccess = dataAccess;
            _userInterface = userInterface;
            _filePath = filePath;
        }

        public void Run()
        {
            SaveTestDrinks();
            while (true)
            {
                _userInterface.DisplayMainMenu();
                CallChosenOptionMethod();
            }

        }

        private void CallChosenOptionMethod()
        {
            Console.Clear();
            string option = Console.ReadLine();

            //validation

            switch (option)
            {
                case "1":
                    BeginTable();
                    break;
                case "2":
                    ShowOpenTables();
                    break;
                case "3":
                    ShowReceipts();
                    break;
                case "q":
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }

        private void BeginTable()
        {
            //showTables
            //chooseTable
            //showMenu
            //chooseProducts
            throw new NotImplementedException();
        }

        private void ShowOpenTables()
        {
            throw new NotImplementedException();
        }

        private void ShowReceipts()
        {
            throw new NotImplementedException();
        }

        private void SaveTestDrinks()
        {
            var drinks = new List<Product>()
            {
                new Drink ("Cappucino", 2.5m),
                new Drink ("Mojito", 9),
                new Drink ("Aperol Spritz", 12),
            };

            _dataAccess.WriteCsv(_filePath, drinks);
        }

    }
}
