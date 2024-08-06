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
        private readonly string _mealsFilePath;

        public ProductService(IDataAccess dataAccess, string drinksFilePath, string mealsFilePath)
        {
            _dataAccess = dataAccess;
            _drinksFilePath = drinksFilePath;
            _mealsFilePath = mealsFilePath;
            //_products = _dataAccess.ReadCsv<Product>(_filePath);
        }

        public List<Product> GetProducts(string filename)
        {
            throw new NotImplementedException();
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
                new Meal("Saltibarsciai", 6),
                new Meal("Balandeliai", 12),
                new Meal("Kepta duona", 6),
                new Meal("Medaus pyragas", 6),
            };

            _dataAccess.WriteCsv(_mealsFilePath, food);
        }
    }
}
