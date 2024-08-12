using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;

namespace AdvancedEgzaminas_Restoranas.Services.Tests
{
    [TestClass()]
    public class TableServiceTests
    {
        [TestMethod()]
        public void GetTableTest_ReturnsCorrectTable()
        {
            // Arrange
            var testTables = new List<Table>()
            {
                new Table(1, 2, false),
                new Table(2, 2, true)
            };

            var service = new TableService(new FakeDataAccess(testTables), "fakePath");
            int expectedTableNumber = 1;

            // Act 
            var table = service.GetTable(1);

            // Assert
            Assert.IsNotNull(table);
            Assert.AreEqual(expectedTableNumber, table.Number);

        }

        [TestMethod()]
        public void GetTableTest_ReturnsNullForNonExistingTable()
        {
            // Arrange
            var testTables = new List<Table>()
            {
                new Table(1, 2, false),
                new Table(2, 2, true)
            };

            var service = new TableService(new FakeDataAccess(testTables), "fakePath");

            // Act 
            var table = service.GetTable(99);

            // Assert
            Assert.IsNull(table);
        }

        [TestMethod()]
        public void GetTableTest_ReturnsNullWhenListIsEmpty()
        {
            // Arrange
            var testTables = new List<Table>();
            var service = new TableService(new FakeDataAccess(testTables), "fakePath");

            // Act 
            var table = service.GetTable(1);

            // Assert
            Assert.IsNull(table);
        }

        [TestMethod()]
        public void GetTableTest_ReturnsCorrectOccupiedStatus()
        {
            // Arrange
            var testTables = new List<Table>()
            {
                new Table(1, 2, false),
                new Table(2, 2, true)
            };

            var service = new TableService(new FakeDataAccess(testTables), "fakePath");
            bool expectedOccupiedStatus = true;

            // Act 
            var table = service.GetTable(2);

            // Assert
            Assert.IsNotNull(table);
            Assert.AreEqual(expectedOccupiedStatus, table.IsOccupied);
        }

        [TestMethod()]
        public void GetTableTest_ReturnsCorrectSeats()
        {
            // Arrange
            var testTables = new List<Table>()
            {
                new Table(1, 2, false),
                new Table(2, 4, true)
            };

            var service = new TableService(new FakeDataAccess(testTables), "fakePath");
            int expectedSeats = 4;

            // Act 
            var table = service.GetTable(2);

            // Assert
            Assert.IsNotNull(table);
            Assert.AreEqual(expectedSeats, table.Seats);
        }

        [TestMethod()]
        public void GetTableTest_ReturnsFirstTableCorrectly()
        {
            // Arrange
            var testTables = new List<Table>()
            {
                new Table(1, 2, false),
                new Table(2, 4, true)
            };

            var service = new TableService(new FakeDataAccess(testTables), "fakePath");

            // Act 
            var table = service.GetTable(1);

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
            var testTables = new List<Table>()
            {
                new Table(1, 2, false),
                new Table(2, 4, true)
            };

            var service = new TableService(new FakeDataAccess(testTables), "fakePath");

            // Act 
            var table = service.GetTable(2);

            // Assert
            Assert.IsNotNull(table);
            Assert.AreEqual(2, table.Number);
            Assert.AreEqual(4, table.Seats);
            Assert.AreEqual(true, table.IsOccupied);
        }

        [TestMethod()]
        public void GetTableTest_HandlesNegativeTableNumber()
        {
            // Arrange
            var testTables = new List<Table>()
            {
                new Table(1, 2, false),
                new Table(2, 4, true)
            };

            var service = new TableService(new FakeDataAccess(testTables), "fakePath");

            // Act 
            var table = service.GetTable(-1);

            // Assert
            Assert.IsNull(table);
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