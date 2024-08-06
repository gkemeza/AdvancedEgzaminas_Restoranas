using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas.Services
{
    // to not write code in program.cs
    public class RestaurantService : IRestaurantService
    {
        private readonly IDataAccess _dataAccess;
        private readonly UserInterface _userInterface;
        private readonly ITableService _tableService;
        private readonly IProductService _productService;

        public RestaurantService(IDataAccess dataAccess, UserInterface userInterface,
            ITableService tableService, IProductService productService)
        {
            _dataAccess = dataAccess;
            _userInterface = userInterface;
            _tableService = tableService;
            _productService = productService;
        }

        public void Run()
        {
            _productService.SeedDrinks();
            _productService.SeedMeals();
            _tableService.SeedTables();

            while (true)
            {
                _userInterface.DisplayMainMenu();
                CallChosenOptionMethod();
            }

        }

        private void CallChosenOptionMethod()
        {
            Console.WriteLine();
            string option = Console.ReadLine();
            // TODO: validate input

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

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void BeginTable()
        {
            int number = _tableService.ChooseTable();
            _tableService.OccupyTable(number);
            while (true)
            {
                // HandleOrderMenu();
                {
                    // AddProduct();
                    {
                        // DisplayProductsMenu();
                    }
                    // Service();
                }
            }
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


    }
}
