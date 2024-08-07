using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;

namespace AdvancedEgzaminas_Restoranas.Services
{
    //nereikia?
    public class ProductService : IProductService
    {
        private List<Product> _products;
        private readonly IDataAccess _dataAccess;
        private readonly string _drinksFilePath;
        private readonly string _foodFilePath;

        public ProductService(IDataAccess dataAccess, string drinksFilePath, string foodFilePath)
        {
            _dataAccess = dataAccess;
            _drinksFilePath = drinksFilePath;
            _foodFilePath = foodFilePath;
        }

        public List<Product> GetProducts(string drinksFilePath, string foodFilePath)
        {
            _products = GetDrinks(drinksFilePath);
            _products.AddRange(GetFood(foodFilePath));
            return _products;
        }

        private List<Product> GetDrinks(string filePath)
        {
            return _dataAccess.ReadCsv<Drink>(filePath).Cast<Product>().ToList();
        }

        private List<Product> GetFood(string filePath)
        {
            return _dataAccess.ReadCsv<Food>(filePath).Cast<Product>().ToList();
        }

        public Product AddProduct()
        {
            var allProducts = GetProducts(_drinksFilePath, _foodFilePath);
            DisplayProductsMenu(allProducts);
            return ChooseProduct(allProducts);
        }

        private Product ChooseProduct(List<Product> products)
        {
            string chosenName = PromptForProductName();

            Product? product = null;
            if (products.Any(p => string.Equals(p.Name, chosenName, StringComparison.OrdinalIgnoreCase)))
            {
                product = products.FirstOrDefault(p => string.Equals(p.Name, chosenName, StringComparison.OrdinalIgnoreCase));
                Console.WriteLine($"{product.Name} was addded to the order");
            }
            else
            {
                Console.WriteLine("Wrong name!");
            }
            return product;
        }

        private string PromptForProductName()
        {
            Console.WriteLine("Enter product name:");
            return Console.ReadLine();
        }


        private void DisplayProductsMenu(List<Product> products)
        {
            var menuItems = GetProducts(_drinksFilePath, _foodFilePath);

            if (menuItems.Count == 0)
            {
                Console.WriteLine("No food items found.");
                return;
            }

            Console.Clear();
            Console.WriteLine("***** Menu *****");
            Console.WriteLine("ID | Name | Price");
            Console.WriteLine("-----------------");

            int i = 1;
            foreach (var item in menuItems)
            {
                Console.WriteLine($"{i++} | {item.Name} | ${item.Price:F2}");
            }
        }

        public void SeedDrinks()
        {
            var drinks = new List<Product>()
            {
                new Drink ("Cappuccino", 2.5m),
                new Drink ("Mojito", 9),
                new Drink ("Aperol Spritz", 12),
            };

            _dataAccess.WriteCsv(_drinksFilePath, drinks);
        }

        public void SeedFood()
        {
            var food = new List<Product>()
            {
                new Food("Saltibarsciai", 6),
                new Food("Balandeliai", 12),
                new Food("Kepta duona", 6),
                new Food("Medaus pyragas", 6),
            };

            _dataAccess.WriteCsv(_foodFilePath, food);
        }
    }
}
