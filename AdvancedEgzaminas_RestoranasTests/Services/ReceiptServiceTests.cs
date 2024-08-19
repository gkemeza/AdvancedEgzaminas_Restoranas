using AdvancedEgzaminas_Restoranas.Enums;
using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas.Services.Tests
{
    [TestClass]
    public class ReceiptServiceTests
    {
        private IReceiptService _receiptService;
        private FakeDataAccess<Receipt> _fakeDataAccess;

        [TestInitialize()]
        public void Setup()
        {
            _fakeDataAccess = new FakeDataAccess<Receipt>();
            _receiptService = new ReceiptService(
                _fakeDataAccess,
                new UserInterface(),
                "receiptsFakePath");
        }

        [TestMethod]
        public void HandleRestaurantReceipt_CreatesCorrectReceipt()
        {
            // Arrange
            var order = CreateSampleOrder();

            // Act
            var result = _receiptService.HandleRestaurantReceipt(order);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ReceiptType.Restaurant, result.Type);
            Assert.AreEqual(order, result.Order);
            Assert.IsTrue(_fakeDataAccess.WasAddReceiptCalled);
        }

        [TestMethod]
        public void HandleClientReceipt_CreatesCorrectReceipt()
        {
            // Arrange
            var order = CreateSampleOrder();

            // Act
            var result = _receiptService.HandleClientReceipt(order);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ReceiptType.Client, result.Type);
            Assert.AreEqual(order, result.Order);
            Assert.IsTrue(_fakeDataAccess.WasAddReceiptCalled);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HandleRestaurantReceipt_NullOrder_ThrowsArgumentNullException()
        {
            // Act
            _receiptService.HandleRestaurantReceipt(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HandleClientReceipt_NullOrder_ThrowsArgumentNullException()
        {
            // Act
            _receiptService.HandleClientReceipt(null);
        }

        [TestMethod]
        public void GetAllReceipts_ReturnsAllOrders()
        {
            // Arrange
            var order = new Order(new Table(1, 4), new List<Product>(), 100m, DateTime.Now);
            var order2 = new Order(new Table(2, 2), new List<Product>(), 50m, DateTime.Now);
            var expectedReceipts = new List<Receipt>
            {
                new Receipt(order, ReceiptType.Restaurant),
                new Receipt(order2, ReceiptType.Client),
            };
            _fakeDataAccess.SetTestData(expectedReceipts);

            // Act
            var result = _receiptService.GetAllReceipts();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedReceipts.Count, result.Count);
            CollectionAssert.AreEqual(expectedReceipts, result);
        }

        [TestMethod]
        public void GetAllReceipts_EmptyList_ReturnsEmptyList()
        {
            // Arrange
            _fakeDataAccess.SetTestData(new List<Receipt>());

            // Act
            var result = _receiptService.GetAllReceipts();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void GetAllReceipts_DataAccessThrowsException_PropagatesException()
        {
            // Arrange
            _fakeDataAccess.ShouldThrowException = true;

            // Act
            _receiptService.GetAllReceipts();
        }

        private Order CreateSampleOrder()
        {
            return new Order(
                new Table(1, 4),
                new List<Product> { new Drink("Fresh juice", 10.0m) },
                10.0m,
                DateTime.Now
            );
        }
    }

}
