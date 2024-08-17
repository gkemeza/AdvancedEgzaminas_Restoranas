using AdvancedEgzaminas_Restoranas.DataAccess;
using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas.Services
{
    public class ProductService : IProductService
    {
        private List<Product> _products;
        private readonly IDataAccess _dataAccess;
        private readonly UserInterface _userInterface;
        private readonly string _drinksFilePath;
        private readonly string _foodFilePath;

        public ProductService(IDataAccess dataAccess, UserInterface userInterface, string drinksFilePath, string foodFilePath)
        {
            _dataAccess = dataAccess;
            _userInterface = userInterface;
            _drinksFilePath = drinksFilePath;
            _foodFilePath = foodFilePath;
        }

        private List<Product> GetProducts()
        {
            _products = GetDrinks(_drinksFilePath);
            _products.AddRange(GetFood(_foodFilePath));
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
            var allProducts = GetProducts();
            _userInterface.DisplayProductsMenu(allProducts);
            return _userInterface.ChooseProduct(allProducts);
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
