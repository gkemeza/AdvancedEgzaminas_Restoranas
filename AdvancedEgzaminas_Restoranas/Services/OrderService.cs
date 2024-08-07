using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.UI;
using System.Text.Json;

namespace AdvancedEgzaminas_Restoranas.Services
{
    // save to orders.csv
    public class OrderService : IOrderService
    {
        //private List<Order> _orders;
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

            while (option != "q" && option != "2")
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
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    case "2":
                        Service(tableNumber, products);
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        break;
                    case "q":
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
        }

        private void UpdateOrders(Order order)
        {
            List<Order> orders = ReadOrdersJson();
            orders.Add(order);
            WriteOrdersJson(orders);
        }

        public List<Order> ReadOrdersJson()
        {
            var orders = new List<Order>();
            try
            {
                if (File.Exists(_ordersFilePath))
                {
                    string[] lines = File.ReadAllLines(_ordersFilePath);
                    foreach (string line in lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            Order order = JsonSerializer.Deserialize<Order>(line, GetJsonSerializerOptions());
                            orders.Add(order);
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"File not found: {e}");
            }
            catch (JsonException e)
            {
                Console.WriteLine($"Deserialization error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
            return orders;
        }

        private JsonSerializerOptions GetJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new ProductConverter() }
            };
            return options;
        }

        private void WriteOrdersJson(List<Order> orders)
        {
            var lines = orders.Select(order => JsonSerializer.Serialize(order));
            File.WriteAllLines(_ordersFilePath, lines);
        }
    }
}
