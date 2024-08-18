using AdvancedEgzaminas_Restoranas.DataAccess;
using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Tests
{
    [TestClass()]
    public class ProductServiceTests
    {
        [TestMethod]
        public void GetProducts_ReturnsCorrectProducts()
        {
            // Arrange
            var mockDataAccess = new MockDataAccess();
            var drinksFilePath = "drinks.csv";
            var foodFilePath = "food.csv";

            mockDataAccess.FileContents[drinksFilePath] = new List<object>
        {
            new Drink { Name = "Coffee" },
            new Drink { Name = "Tea" }
        };

            mockDataAccess.FileContents[foodFilePath] = new List<object>
        {
            new Food { Name = "Sandwich" },
            new Food { Name = "Cake" }
        };

            var productService = new ProductService(mockDataAccess, drinksFilePath, foodFilePath);

            // Act
            var result = productService.GetProducts();

            // Assert
            Assert.AreEqual(4, result.Count);
            Assert.IsTrue(result.Any(p => p.Name == "Coffee" && p is Drink));
            Assert.IsTrue(result.Any(p => p.Name == "Tea" && p is Drink));
            Assert.IsTrue(result.Any(p => p.Name == "Sandwich" && p is Food));
            Assert.IsTrue(result.Any(p => p.Name == "Cake" && p is Food));
        }

        [TestMethod]
        public void GetProducts_EmptyFiles_ReturnsEmptyList()
        {
            // Arrange
            var mockDataAccess = new MockDataAccess();
            var drinksFilePath = "empty_drinks.csv";
            var foodFilePath = "empty_food.csv";

            mockDataAccess.FileContents[drinksFilePath] = new List<object>();
            mockDataAccess.FileContents[foodFilePath] = new List<object>();

            var productManager = new ProductService(mockDataAccess, drinksFilePath, foodFilePath);

            // Act
            var result = productManager.GetProducts();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetProducts_OnlyDrinks_ReturnsOnlyDrinks()
        {
            // Arrange
            var mockDataAccess = new MockDataAccess();
            var drinksFilePath = "drinks.csv";
            var foodFilePath = "empty_food.csv";

            mockDataAccess.FileContents[drinksFilePath] = new List<object>
        {
            new Drink { Name = "Coffee" },
            new Drink { Name = "Tea" }
        };
            mockDataAccess.FileContents[foodFilePath] = new List<object>();

            var productManager = new ProductService(mockDataAccess, drinksFilePath, foodFilePath);

            // Act
            var result = productManager.GetProducts();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(p => p is Drink));
        }

        [TestMethod]
        public void GetProducts_OnlyFood_ReturnsOnlyFood()
        {
            // Arrange
            var mockDataAccess = new MockDataAccess();
            var drinksFilePath = "empty_drinks.csv";
            var foodFilePath = "food.csv";

            mockDataAccess.FileContents[drinksFilePath] = new List<object>();
            mockDataAccess.FileContents[foodFilePath] = new List<object>
        {
            new Food { Name = "Sandwich" },
            new Food { Name = "Cake" }
        };

            var productManager = new ProductService(mockDataAccess, drinksFilePath, foodFilePath);

            // Act
            var result = productManager.GetProducts();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(p => p is Food));
        }

        private class MockDataAccess : IDataAccess
        {
            public Dictionary<string, List<object>> FileContents { get; } = new Dictionary<string, List<object>>();

            public void AddReceipt(Receipt receipt, string filePath)
            {
                throw new NotImplementedException();
            }

            public List<T> ReadCsv<T>(string filePath)
            {
                if (FileContents.TryGetValue(filePath, out var contents))
                {
                    return contents.Cast<T>().ToList();
                }
                return new List<T>();
            }

            public List<T> ReadJson<T>(string filePath)
            {
                throw new NotImplementedException();
            }

            public void WriteCsv<T>(string filePath, List<T> data)
            {
                throw new NotImplementedException();
            }

            public void WriteJson<T>(string filePath, List<T> data)
            {
                throw new NotImplementedException();
            }
        }
    }
}