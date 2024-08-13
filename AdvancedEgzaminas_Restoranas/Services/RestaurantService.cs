﻿using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas.Services
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
                case "4":
                    ViewTables();
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
            int number = _tableService.ChooseTable();
            if (_tableService.IsTableAvailable(number))
            {
                _tableService.OccupyTable(number);
                _orderService.HandleOrderMenu(number);
            }
            else
            {
                Console.WriteLine("Table is taken!");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private void ShowOpenTables()
        {
            Console.Clear();
            var orders = _orderService.GetOrders();

            _orderService.PrintOrders(orders);

            FinishOrder();

        }

        private void ShowReceipts()
        {
            Console.Clear();
            var receipts = _receiptService.GetReceipts();
            if (receipts.Count == 0)
            {
                Console.WriteLine("No receipts found.");
                return;
            }
            Console.WriteLine("***** Receipts *****\n");
            foreach (Receipt receipt in receipts)
            {
                Console.WriteLine($"* {receipt.Type} receipt *");
                Console.WriteLine($"ID: {receipt.Id}");
                Console.WriteLine($"Order: {receipt.Order}");

                Console.WriteLine(new string('-', 40));
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void FinishOrder()
        {
            int tableNumber = _userInterface.PromptForTableNumber();

            var order = _orderService.GetOrder(tableNumber);
            if (order != null)
            {
                _receiptService.HandleRestaurantReceipt(order);
                _orderService.EndOrder(tableNumber);
            }
            else
            {
                Console.WriteLine("Wrong table number!");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private void ViewTables()
        {
            Console.Clear();
            _tableService.PrintTables();

            Console.WriteLine("\nPress any key to go back.");
            Console.ReadKey();
        }

    }
}
