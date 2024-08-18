using AdvancedEgzaminas_Restoranas.DataAccess;
using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IDataAccess _dataAccess;
        private readonly UserInterface _userInterface;
        private readonly ITableService _tableService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IReceiptService _receiptService;

        public RestaurantService(IDataAccess dataAccess, UserInterface userInterface,
            ITableService tableService, IProductService productService,
            IOrderService orderService, IReceiptService receiptService)
        {
            _dataAccess = dataAccess;
            _userInterface = userInterface;
            _tableService = tableService;
            _productService = productService;
            _orderService = orderService;
            _receiptService = receiptService;
        }

        public void Run()
        {
            //_productService.SeedDrinks();
            //_productService.SeedFood();
            //_tableService.SeedTables();

            while (true)
            {
                _userInterface.DisplayMainMenu();
                CallChosenOptionMethod();
            }
        }

        private void CallChosenOptionMethod()
        {
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    BeginTable();
                    break;
                case "2":
                    ShowOpenTables();
                    break;
                case "3":
                    _receiptService.ShowReceipts();
                    break;
                case "4":
                    ViewTables();
                    break;
                case "q":
                    Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        private void BeginTable()
        {
            int number = _tableService.ChooseTable();
            _tableService.OccupyTable(number);
            _tableService.UpdateTablesInFile();
            _orderService.HandleOrderMenu(number);
        }

        private void ShowOpenTables()
        {
            Console.Clear();
            var orders = _orderService.GetOrders();

            _orderService.PrintOrders(orders);

            FinishOrder();

        }

        private void FinishOrder()
        {
            int tableNumber = _userInterface.PromptForTableNumber();

            var order = _orderService.GetOrder(tableNumber);
            if (order != null)
            {
                _receiptService.HandleRestaurantReceipt(order);
                if (_userInterface.IsClientReceiptNeeded())
                {
                    _receiptService.HandleClientReceipt(order);
                }
                _receiptService.SendEmail();

                _orderService.EndOrder(tableNumber);

                _userInterface.DisplayMessageAndWait("Order was finished.");
            }
            else
            {
                _userInterface.DisplayMessageAndWait("Wrong table number!");
            }
        }

        private void ViewTables()
        {
            Console.Clear();
            _tableService.PrintTables();
            _userInterface.DisplayMessageAndWait(string.Empty);
        }

    }
}
