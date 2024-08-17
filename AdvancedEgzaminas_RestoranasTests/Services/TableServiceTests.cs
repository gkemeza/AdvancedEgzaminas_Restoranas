using AdvancedEgzaminas_Restoranas.DataAccess;
using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas.Services.Tests
{
    [TestClass()]
    public class TableServiceTests
    {
        private TableService _tableService;

        [TestInitialize()]
        public void Setup()
        {
            var testTables = new List<Table>()
            {
                new Table(1, 2, false),
                new Table(2, 4, true),
            };

            _tableService = new TableService(new FakeDataAccess(testTables), new UserInterface(), "fakePath");
        }

        [TestMethod()]
        public void GetTableTest_ReturnsCorrectTable()
        {
            // Arrange
            int tableNumber = 1;
            int expectedTableNumber = 1;

            // Act 
            var table = _tableService.GetTable(tableNumber);

            // Assert
            Assert.IsNotNull(table);
            Assert.AreEqual(expectedTableNumber, table.Number);

        }

        [TestMethod()]
        public void GetTableTest_ReturnsNullForNonExistingTable()
        {
            // Arrange
            int tableNumber = 99;

            // Act 
            var table = _tableService.GetTable(tableNumber);

            // Assert
            Assert.IsNull(table);
        }

        [TestMethod()]
        public void GetTableTest_ReturnsNullWhenListIsEmpty()
        {
            // Arrange
            var testTables = new List<Table>();
            _tableService = new TableService(new FakeDataAccess(testTables), new UserInterface(), "fakePath");
            int tableNumber = 1;

            // Act 
            var table = _tableService.GetTable(tableNumber);

            // Assert
            Assert.IsNull(table);
        }

        [TestMethod()]
        public void GetTableTest_ReturnsCorrectOccupiedStatus()
        {
            // Arrange
            int tableNumber = 2;
            bool expectedOccupiedStatus = true;

            // Act 
            var table = _tableService.GetTable(tableNumber);

            // Assert
            Assert.IsNotNull(table);
            Assert.AreEqual(expectedOccupiedStatus, table.IsOccupied);
        }

        [TestMethod()]
        public void GetTableTest_ReturnsCorrectSeats()
        {
            // Arrange
            int tableNumber = 2;
            int expectedSeats = 4;

            // Act 
            var table = _tableService.GetTable(tableNumber);

            // Assert
            Assert.IsNotNull(table);
            Assert.AreEqual(expectedSeats, table.Seats);
        }

        [TestMethod()]
        public void GetTableTest_ReturnsFirstTableCorrectly()
        {
            // Arrange
            int tableNumber = 1;

            // Act 
            var table = _tableService.GetTable(tableNumber);

            // Assert
            Assert.IsNotNull(table);
            Assert.AreEqual(1, table.Number);
            Assert.AreEqual(2, table.Seats);
            Assert.AreEqual(false, table.IsOccupied);
        }

        [TestMethod()]
        public void GetTableTest_ReturnsLastTableCorrectly()
        {
            // Arrange
            int tableNumber = 2;

            // Act 
            var table = _tableService.GetTable(tableNumber);

            // Assert
            Assert.IsNotNull(table);
            Assert.AreEqual(2, table.Number);
            Assert.AreEqual(4, table.Seats);
            Assert.AreEqual(true, table.IsOccupied);
        }

        [TestMethod()]
        public void GetTableTest_ReturnsNullIfNegativeTableNumber()
        {
            // Arrange
            int tableNumber = -1;

            // Act 
            var table = _tableService.GetTable(tableNumber);

            // Assert
            Assert.IsNull(table);
        }

        [TestMethod()]
        public void IsTableAvailableTest_ReturnsTrueIfFreeTableExists()
        {
            // Arrange
            int tableNumber = 1;

            // Act
            bool result = _tableService.IsTableAvailable(tableNumber);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsTableAvailableTest_ReturnsFalseIfOccupiedTableExists()
        {
            // Arrange
            int tableNumber = 2;

            // Act
            bool result = _tableService.IsTableAvailable(tableNumber);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsTableAvailableTest_ReturnsFalseIfTableDoesNotExist()
        {
            // Arrange
            int tableNumber = 99;

            // Act
            bool result = _tableService.IsTableAvailable(tableNumber);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsTableAvailableTest_ReturnsFalseIfEmptyTableList()
        {
            // Arrange
            var testTables = new List<Table>();
            _tableService = new TableService(new FakeDataAccess(testTables), new UserInterface(), "fakePath");
            int tableNumber = 1;

            // Act
            bool result = _tableService.IsTableAvailable(tableNumber);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FreeTableTest_ReturnsTrueIfOccupiedTableIsValid()
        {
            // Arrange
            int tableNumber = 2;

            // Act
            bool result = _tableService.FreeTable(tableNumber);

            // Assert
            Assert.IsTrue(result);
            Assert.IsFalse(_tableService.GetTable(tableNumber).IsOccupied);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FreeTableTest_ThrowsArgumentExceptionIfTableDoesNotExist()
        {
            // Arrange
            int tableNumber = 99;

            // Act
            _tableService.FreeTable(tableNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FreeTableTest_ThrowsInvalidOperationExceptionIfTableIsAlreadyFree()
        {
            // Arrange
            int tableNumber = 1;

            // Act
            _tableService.FreeTable(tableNumber);
        }


        [TestMethod]
        public void OccupyTableTest_ReturnsTrueIfUnoccupiedTableIsValid()
        {
            // Arrange
            int tableNumber = 1;

            // Act
            bool result = _tableService.OccupyTable(tableNumber);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(_tableService.GetTable(tableNumber).IsOccupied);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OccupyTableTest_ThrowsArgumentExceptionIfTableDoesNotExist()
        {
            // Arrange
            int tableNumber = 99;

            // Act
            _tableService.OccupyTable(tableNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void OccupyTableTest_ThrowsInvalidOperationExceptionIfTableIsAlreadyOccupied()
        {
            // Arrange
            int tableNumber = 2;

            // Act
            _tableService.OccupyTable(tableNumber);
        }

    }

    public class FakeDataAccess : IDataAccess
    {
        private readonly List<Table> _testTables;

        public FakeDataAccess(List<Table> testTables)
        {
            _testTables = testTables;
        }

        public List<T> ReadCsv<T>(string filePath)
        {
            // Simulate reading from a file by returning the in-memory list
            return _testTables.Cast<T>().ToList();
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