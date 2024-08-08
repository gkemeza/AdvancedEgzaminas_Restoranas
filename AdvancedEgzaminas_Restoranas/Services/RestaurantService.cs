﻿using AdvancedEgzaminas_Restoranas.Services.Interfaces;
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
        private readonly IOrderService _orderService;

        public RestaurantService(IDataAccess dataAccess, UserInterface userInterface,
            ITableService tableService, IProductService productService, IOrderService orderService)
        {
            _dataAccess = dataAccess;
            _userInterface = userInterface;
            _tableService = tableService;
            _productService = productService;
            _orderService = orderService;
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
            var orders = _orderService.ReadOrdersJson();
            if (orders.Count == 0)
            {
                Console.WriteLine("No open tables found.");
                return;
            }
            Console.WriteLine("***** Open Tables *****\n");
            foreach (var order in orders)
            {
                Console.WriteLine($"Table Number: {order.Table.Number}");
                Console.WriteLine($"Seats: {order.Table.Seats}");
                Console.WriteLine($"Order Time: {order.OrderTime}");
                Console.WriteLine("Products:");
                foreach (var product in order.Products)
                {
                    Console.WriteLine($"- {product.Name} ({product.Type}): {product.Price:C}");
                }
                Console.WriteLine($"Total Amount: {order.TotalAmount:C}");
                Console.WriteLine(new string('-', 40));
            }
            _orderService.FinishOrder();

            //Console.WriteLine("\nPress any key to continue...");
            //Console.ReadKey();
        }

        private void ShowReceipts()
        {
            throw new NotImplementedException();
        }


    }
}
