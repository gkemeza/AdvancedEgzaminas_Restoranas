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

        public List<Product> GetProducts(string filePath)
        {
            return _dataAccess.ReadCsv<Product>(filePath);
        }

        public void AddProduct()
        {
            DisplayProductsMenu();
            // ChooseProduct();
        }


        private void DisplayProductsMenu()
        {
            var drinks = GetProducts(_drinksFilePath);

            if (drinks.Count == 0)
            {
                Console.WriteLine("No food items found.");
                return;
            }

            Console.WriteLine("***** Menu *****");
            Console.WriteLine("ID | Name | Price");
            Console.WriteLine("-----------------");

            int i = 1;
            foreach (var item in drinks)
            {
                Console.WriteLine($"{i++} | {item.Name} | ${item.Price:F2}");
            }
        }

        public void SeedDrinks()
        {
            var drinks = new List<Product>()
            {
                new Drink ("Cappucino", 2.5m),
                new Drink ("Mojito", 9),
                new Drink ("Aperol Spritz", 12),
            };

            _dataAccess.WriteCsv(_drinksFilePath, drinks);
        }

        public void SeedMeals()
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
