using AdvancedEgzaminas_Restoranas.DataAccess;
using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas.Services.Tests
{
    [TestClass()]
    public class ProductServiceTests
    {
        private IProductService _productService;

        [TestMethod()]
        public void GetProductsTest()
        {
            Assert.Fail();
        }

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

        private class MockDataAccess : IDataAccess
        {
            public Dictionary<string, List<object>> FileContents { get; } = new Dictionary<string, List<object>>();

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