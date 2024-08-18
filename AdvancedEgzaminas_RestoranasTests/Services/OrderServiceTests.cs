using AdvancedEgzaminas_Restoranas.DataAccess;
using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas.Services.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        private IOrderService _orderService;
        private IDataAccess _dataAccess;

        [TestInitialize()]
        public void Setup()
        {
            _dataAccess = new DataAccess.DataAccess();
            _orderService = new OrderService(
                _dataAccess,
                new FakeTableService(),
                new ProductService(_dataAccess, "drinksFakePath", "foodFakePath"),
                new UserInterface(),
                "ordersFakePath");
        }

        [TestMethod]
        public void CreateOrder_WithValidInputs_ReturnsCorrectOrder()
        {
            // Arrange
            int tableNumber = 1;
            var products = new List<Product>
            {
                new Drink("Fresh juice", 10.0m),
                new Food("Soup", 15.0m)
            };

            // Act
            var result = _orderService.CreateOrder(tableNumber, products);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(tableNumber, result.Table.Number);
            Assert.AreEqual(products.Count, result.Products.Count);
            Assert.AreEqual(25.0m, result.TotalAmount);
            Assert.IsTrue((DateTime.Now - result.OrderTime).TotalSeconds < 1);
        }

        [TestMethod]
        public void CreateOrder_WithEmptyProductList_ReturnsOrderWithZeroTotal()
        {
            // Arrange
            int tableNumber = 2;
            var products = new List<Product>();

            // Act
            var result = _orderService.CreateOrder(tableNumber, products);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(tableNumber, result.Table.Number);
            Assert.AreEqual(0, result.Products.Count);
            Assert.AreEqual(0m, result.TotalAmount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateOrder_WithNullProductList_ThrowsArgumentNullException()
        {
            // Arrange
            int tableNumber = 3;

            // Act
            _orderService.CreateOrder(tableNumber, null);
        }

        [TestMethod]
        public void CreateOrder_VerifiesTableNumberIsCorrect()
        {
            // Arrange
            int tableNumber = 4;
            var products = new List<Product> { new Drink("Fresh juice", 5.0m) };

            // Act
            var result = _orderService.CreateOrder(tableNumber, products);

            // Assert
            Assert.AreEqual(tableNumber, result.Table.Number);
        }
    }

    public class FakeTableService : ITableService
    {
        public Table GetTable(int tableNumber)
        {
            return new Table(tableNumber, 4);
        }

        public int ChooseTable()
        {
            throw new NotImplementedException();
        }

        public bool FreeTable(int tableNumber)
        {
            throw new NotImplementedException();
        }

        public bool IsTableAvailable(int tableNumber)
        {
            throw new NotImplementedException();
        }

        public bool OccupyTable(int tableNumber)
        {
            throw new NotImplementedException();
        }

        public void PrintTables()
        {
            throw new NotImplementedException();
        }

        public void SeedTables()
        {
            throw new NotImplementedException();
        }

        public void UpdateTablesInFile()
        {
            throw new NotImplementedException();
        }
    }
}