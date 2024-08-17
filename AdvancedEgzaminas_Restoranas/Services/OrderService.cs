using AdvancedEgzaminas_Restoranas.DataAccess;
using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas.Services
{
    public class OrderService : IOrderService
    {
        private readonly IDataAccess _dataAccess;
        private readonly ITableService _tableService;
        private readonly IProductService _productService;
        private readonly UserInterface _userInterface;
        private readonly string _ordersFilePath;

        public OrderService(IDataAccess dataAccess, ITableService tableService, IProductService productService, UserInterface userInterface, string filePath)
        {
            _dataAccess = dataAccess;
            _tableService = tableService;
            _productService = productService;
            _userInterface = userInterface;
            _ordersFilePath = filePath;
        }

        public Order CreateOrder(int tableNumber, List<Product> products)
        {
            Table table = _tableService.GetTable(tableNumber);

            decimal totalPrice = 0;
            foreach (var product in products)
            {
                totalPrice += product.Price;
            }

            return new Order(table, products, totalPrice, DateTime.Now);
        }

        public void HandleOrderMenu(int tableNumber)
        {
            string option = string.Empty;
            var products = new List<Product>();

            while (option != "2")
            {
                _userInterface.DisplayOrderMenu();

                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        var prod = _productService.AddProduct();
                        if (prod != null)
                        {
                            products.Add(prod);
                        }
                        Console.WriteLine("\nPress 'Enter' to continue...");
                        Console.ReadLine();
                        break;
                    case "2":
                        Service(tableNumber, products);
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }

        public void Service(int tableNumber, List<Product> products)
        {
            Order order = CreateOrder(tableNumber, products);
            UpdateOrders(order);
            Console.WriteLine("\nOrder was created");
            Console.WriteLine("\nPress 'Enter' to continue...");
            Console.ReadLine();
        }

        private void UpdateOrders(Order order)
        {
            List<Order> orders = _dataAccess.ReadJson<Order>(_ordersFilePath);
            orders.Add(order);
            _dataAccess.WriteJson<Order>(_ordersFilePath, orders);
        }

        public void EndOrder(int tableNumber)
        {
            RemoveOrder(tableNumber);
            _tableService.FreeTable(tableNumber);
            _tableService.UpdateTablesInFile();
        }

        private void RemoveOrder(int tableNumber)
        {
            try
            {
                var orders = _dataAccess.ReadJson<Order>(_ordersFilePath);
                var orderToRemove = orders.Find(o => o.Table.Number == tableNumber);

                if (orderToRemove != null)
                {
                    orders.Remove(orderToRemove);
                    _dataAccess.WriteJson<Order>(_ordersFilePath, orders);
                }
                else
                {
                    Console.WriteLine($"No order found for table {tableNumber}.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error removing order: {e.Message}");
            }
        }

        public Order GetOrder(int tableNumber)
        {
            var orders = _dataAccess.ReadJson<Order>(_ordersFilePath);
            return orders.FirstOrDefault(o => o.Table.Number == tableNumber);
        }

        public List<Order> GetOrders()
        {
            return _dataAccess.ReadJson<Order>(_ordersFilePath);
        }

        public void PrintOrders(List<Order> orders)
        {
            if (orders.Count == 0)
            {
                Console.WriteLine("No open tables found.");
                return;
            }
            Console.WriteLine("***** Open Tables *****\n");
            foreach (Order order in orders)
            {
                Console.WriteLine($"Table Number: {order.Table.Number}");
                Console.WriteLine($"Seats: {order.Table.Seats}");
                Console.WriteLine($"Order Time: {order.OrderTime}");
                Console.WriteLine("Products:");
                foreach (var product in order.Products)
                {
                    Console.WriteLine($"- {product.Name} ({product.Type}): {product.Price} Eur");
                }
                Console.WriteLine($"Total Amount: {order.TotalAmount} Eur");
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}
