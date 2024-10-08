﻿using AdvancedEgzaminas_Restoranas.DataAccess;
using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;

namespace AdvancedEgzaminas_Restoranas.Services
{
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

        public List<Product> GetProducts()
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

        public void SeedDrinks()
        {
            var drinks = new List<Product>()
            {
                new Drink ("Espresso", 2),
                new Drink ("Juoda kava", 2.5m),
                new Drink ("Cappuccino", 2.5m),
                new Drink ("Arbata", 3.5m),
                new Drink ("Sultys", 3),
                new Drink ("Beer 0.33", 5.50m),
                new Drink ("Beer 0.5", 7),
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
                new Food("Cezario salotos", 6),
                new Food("Kepta duona", 6),
                new Food("Balandeliai", 12),
                new Food("Jautienos mesainis", 14),
                new Food("Medaus pyragas", 6),
                new Food("Ledai", 3)
            };

            _dataAccess.WriteCsv(_foodFilePath, food);
        }
    }
}
