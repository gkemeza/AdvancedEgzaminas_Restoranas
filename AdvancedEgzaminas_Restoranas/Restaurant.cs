using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;

namespace AdvancedEgzaminas_Restoranas
{
    // to not write code in program.cs
    // make this into service class?
    public class Restaurant
    {
        private readonly IDataAccess _dataAccess;
        private readonly string _filePath;

        public Restaurant(IDataAccess dataAccess, string filePath)
        {
            _dataAccess = dataAccess;
            _filePath = filePath;
        }

        private void SaveTestDrinks()
        {
            var drinks = new List<Product>()
            {
                new Drink ("Cappucino", 2.5m),
                new Drink ("Mojito", 9),
                new Drink ("Aperol Spritz", 12),
            };

            _dataAccess.WriteCsv(_filePath, drinks);
        }

        public void Run()
        {
            SaveTestDrinks();
        }
    }
}
