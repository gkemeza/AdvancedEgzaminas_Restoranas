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
        private FakeDataAccess _fakeDataAccess;

        [TestInitialize()]
        public void Setup()
        {
            _fakeDataAccess = new FakeDataAccess();
            _orderService = new OrderService(
                _fakeDataAccess,
                new FakeTableService(),
                new ProductService(_fakeDataAccess, "drinksFakePath", "foodFakePath"),
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

        [TestMethod]
        public void GetOrder_ExistingTableNumber_ReturnsCorrectOrder()
        {
            // Arrange
            int tableNumber = 1;
            var expectedOrder = new Order(new Table(tableNumber, 4), new List<Product>(), 100m, DateTime.Now);
            _fakeDataAccess.SetTestOrders(new List<Order> { expectedOrder });

            // Act
            var result = _orderService.GetOrder(tableNumber);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(tableNumber, result.Table.Number);
            Assert.AreEqual(expectedOrder.TotalAmount, result.TotalAmount);
        }

        [TestMethod]
        public void GetOrder_NonExistingTableNumber_ReturnsNull()
        {
            // Arrange
            int tableNumber = 99;
            var testOrders = new List<Order>
            {
                new Order(new Table(1, 4), new List<Product>(), 100m, DateTime.Now),
                new Order(new Table(2, 2), new List<Product>(), 50m, DateTime.Now)
            };
            _fakeDataAccess.SetTestOrders(testOrders);

            // Act
            var result = _orderService.GetOrder(tableNumber);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetOrder_EmptyOrderList_ReturnsNull()
        {
            // Arrange
            _fakeDataAccess.SetTestOrders(new List<Order>());

            // Act
            var result = _orderService.GetOrder(1);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetOrder_DataAccessThrowsException_PropagatesException()
        {
            // Arrange
            _fakeDataAccess.ShouldThrowException = true;

            // Act
            _orderService.GetOrder(1);
        }
    }

    public class FakeDataAccess : IDataAccess
    {
        private List<Order> _testOrders;
        public bool ShouldThrowException { get; set; }

        public void SetTestOrders(List<Order> orders)
        {
            _testOrders = orders;
        }

        public List<T> ReadJson<T>(string filePath)
        {
            if (ShouldThrowException)
            {
                throw new Exception("Simulated data access error");
            }
            return _testOrders as List<T>;
        }

        public List<T> ReadCsv<T>(string filePath)
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