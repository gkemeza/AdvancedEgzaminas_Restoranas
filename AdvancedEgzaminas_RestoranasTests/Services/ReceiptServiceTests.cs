using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas.Services.Tests
{
    [TestClass]
    public class ReceiptServiceTests
    {
        private IReceiptService _receiptService;
        private FakeDataAccess _fakeDataAccess;

        [TestInitialize()]
        public void Setup()
        {
            _fakeDataAccess = new FakeDataAccess();
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
