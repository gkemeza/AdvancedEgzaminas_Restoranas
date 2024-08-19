using AdvancedEgzaminas_Restoranas.Enums;
using AdvancedEgzaminas_Restoranas.Models;
using CsvHelper;
using CsvHelper.TypeConversion;
using System.Text.Json;

namespace AdvancedEgzaminas_Restoranas.DataAccess.Tests
{
    [TestClass()]
    public class DataAccessTests
    {
        private IDataAccess _dataAccess;

        [TestInitialize]
        public void Setup()
        {
            _dataAccess = new DataAccess();
        }

        [TestMethod]
        public void ReadCsv_FileDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            string filePath = "non_existent_file.csv";

            // Act
            var result = _dataAccess.ReadCsv<TestClass>(filePath);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ReadCsv_FileExists_ReturnsCorrectData()
        {
            // Arrange
            string filePath = "test.csv";
            var csvContent = "Id,Name\n1,John Doe\n2,Jane Doe";
            File.WriteAllText(filePath, csvContent);

            // Act
            var result = _dataAccess.ReadCsv<TestClass>(filePath);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("John Doe", result[0].Name);
            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual("Jane Doe", result[1].Name);

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(BadDataException))]
        public void ReadCsv_FileExistsButMalformedCsv_ThrowsReaderException()
        {
            // Arrange
            string filePath = "test_malformed.csv";
            var csvContent = "Id,Name\n1,\"John Doe\n2,Jane Doe"; // missing closing quote
            File.WriteAllText(filePath, csvContent);

            // Act
            var result = _dataAccess.ReadCsv<TestClass>(filePath);

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(HeaderValidationException))]
        public void ReadCsv_FileExistsButIncorrectFormat_ThrowsHeaderValidationException()
        {
            // Arrange
            string filePath = "test_incorrect.csv";
            var csvContent = "InvalidContent";
            File.WriteAllText(filePath, csvContent);

            // Act
            var result = _dataAccess.ReadCsv<TestClass>(filePath);

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        [ExpectedException(typeof(TypeConverterException))]
        public void ReadCsv_FileExistsButDataFormatIncorrect_ThrowsTypeConverterException()
        {
            // Arrange
            string filePath = "test_incorrect_data.csv";
            var csvContent = "Id,Name\nInvalidId,John Doe"; // "InvalidId" cannot be converted to an int
            File.WriteAllText(filePath, csvContent);

            // Act
            var result = _dataAccess.ReadCsv<TestClass>(filePath);

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        public void ReadJson_FileDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            string filePath = "non_existent_file.json";

            // Act
            var result = _dataAccess.ReadJson<TestClass>(filePath);

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void ReadJson_FileExistsButIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            string filePath = "empty_file.json";
            File.WriteAllText(filePath, "");

            // Act
            var result = _dataAccess.ReadJson<TestClass>(filePath);

            // Assert
            Assert.AreEqual(0, result.Count);

            // Cleanup
            File.Delete(filePath);
        }

        // TODO: fix ReadJson() tests
        [TestMethod]
        public void ReadJson_FileExistsAndContainsValidJson_ReturnsCorrectData()
        {
            // Arrange
            string filePath = "valid_file.json";
            var jsonContent = "{\"Id\":1,\"Name\":\"John Doe\"}\n{\"Id\":2,\"Name\":\"Jane Doe\"}";
            File.WriteAllText(filePath, jsonContent);

            // Act
            var result = _dataAccess.ReadJson<TestClass>(filePath);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual("John Doe", result[0].Name);
            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual("Jane Doe", result[1].Name);

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        public void ReadJson_FileExistsButContainsInvalidJson_CatchesJsonException()
        {
            // Arrange
            string filePath = "invalid_file.json";
            var jsonContent = "{\"Id\":1,\"Name\":\"John Doe\"}\nInvalidJsonLine";
            File.WriteAllText(filePath, jsonContent);

            // Act
            var result = _dataAccess.ReadJson<TestClass>(filePath);

            // Assert
            Assert.AreEqual(1, result.Count);  // Only the first valid line is deserialized

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        public void ReadJson_FileExistsWithMixedJson_CatchesJsonExceptionButContinuesProcessing()
        {
            // Arrange
            string filePath = "mixed_file.json";
            var jsonContent = "{\"Id\":1,\"Name\":\"John Doe\"}\nInvalidJsonLine\n{\"Id\":2,\"Name\":\"Jane Doe\"}";
            File.WriteAllText(filePath, jsonContent);

            // Act
            var result = _dataAccess.ReadJson<TestClass>(filePath);

            // Assert
            Assert.AreEqual(2, result.Count);  // Both valid lines should be deserialized

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        public void ReadJson_FileExistsButUnexpectedExceptionOccurs_ReturnsEmptyList()
        {
            // Arrange
            string filePath = "unexpected_error_file.json";
            var jsonContent = "{\"Id\":1,\"Name\":\"John Doe\"}";
            File.WriteAllText(filePath, jsonContent);

            // Introduce an unexpected error by restricting file access
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                // Act
                var result = _dataAccess.ReadJson<TestClass>(filePath);

                // Assert
                Assert.AreEqual(0, result.Count);
            }

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        public void WriteCsv_FileIsCreatedAndContainsCorrectData()
        {
            // Arrange
            string filePath = "output.csv";
            var data = new List<TestClass>
            {
                new TestClass { Id = 1, Name = "John Doe" },
                new TestClass { Id = 2, Name = "Jane Doe" }
            };

            // Act
            _dataAccess.WriteCsv(filePath, data);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
            var content = File.ReadAllText(filePath);
            var expectedContent = "Id,Name\r\n1,John Doe\r\n2,Jane Doe\r\n";
            Assert.AreEqual(expectedContent, content);

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        public void WriteCsv_EmptyListCreatesEmptyFileWithHeaders()
        {
            // Arrange
            string filePath = "empty_output.csv";
            var data = new List<TestClass>();

            // Act
            _dataAccess.WriteCsv(filePath, data);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
            var content = File.ReadAllText(filePath);
            var expectedContent = "Id,Name\r\n"; // Only headers
            Assert.AreEqual(expectedContent, content);

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        public void WriteCsv_FileIsOverwrittenIfItAlreadyExists()
        {
            // Arrange
            string filePath = "existing_file.csv";
            var initialData = new List<TestClass>
            {
                new TestClass { Id = 1, Name = "John Doe" }
            };
            File.WriteAllText(filePath, "Id,Name\r\n1,Old Data\r\n");

            var newData = new List<TestClass>
            {
                new TestClass { Id = 2, Name = "Jane Doe" }
            };

            // Act
            _dataAccess.WriteCsv(filePath, newData);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
            var content = File.ReadAllText(filePath);
            var expectedContent = "Id,Name\r\n2,Jane Doe\r\n";
            Assert.AreEqual(expectedContent, content);

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        public void WriteJson_FileIsCreatedAndContainsCorrectData()
        {
            // Arrange
            string filePath = "output.json";
            var data = new List<TestClass>
            {
                new TestClass { Id = 1, Name = "John Doe" },
                new TestClass { Id = 2, Name = "Jane Doe" }
            };

            // Act
            _dataAccess.WriteJson(filePath, data);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
            var content = File.ReadAllLines(filePath);
            var expectedContent = data.Select(item => JsonSerializer.Serialize(item)).ToArray();
            CollectionAssert.AreEqual(expectedContent, content);

            // Cleanup
            File.Delete(filePath);
        }

        // TODO: fix WriteJson() tests
        [TestMethod]
        public void WriteJson_EmptyListCreatesEmptyFile()
        {
            // Arrange
            string filePath = "empty_output.json";
            var data = new List<TestClass>();

            // Act
            _dataAccess.WriteJson(filePath, data);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
            var content = File.ReadAllLines(filePath);
            Assert.AreEqual(0, content.Length);

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        public void WriteJson_FileIsOverwrittenIfItAlreadyExists()
        {
            // Arrange
            string filePath = "existing_file.json";
            var initialData = new List<TestClass>
            {
                new TestClass { Id = 1, Name = "Old Data" }
            };
            _dataAccess.WriteJson(filePath, initialData);

            var newData = new List<TestClass>
            {
                new TestClass { Id = 2, Name = "Jane Doe" }
            };

            // Act
            _dataAccess.WriteJson(filePath, newData);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
            var content = File.ReadAllLines(filePath);
            var expectedContent = newData.Select(item => JsonSerializer.Serialize(item)).ToArray();
            CollectionAssert.AreEqual(expectedContent, content);

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        public void AddReceipt_FileExists_AddsReceiptAndUpdatesFile()
        {
            // Arrange
            string filePath = "receipts.json";
            var order = new Order(new Table(1, 4), new List<Product>(), 100m, DateTime.Now);
            var order2 = new Order(new Table(2, 2), new List<Product>(), 50m, DateTime.Now);
            var existingReceipts = new List<Receipt>
            {
                new Receipt(order, ReceiptType.Restaurant),
                new Receipt(order2, ReceiptType.Client),
            };
            _dataAccess.WriteJson(filePath, existingReceipts);

            var order3 = new Order(new Table(2, 2), new List<Product>(), 50m, DateTime.Now);
            var newReceipt = new Receipt(order3, ReceiptType.Restaurant);

            // Act
            _dataAccess.AddReceipt(newReceipt, filePath);

            // Assert
            var updatedReceipts = _dataAccess.ReadJson<Receipt>(filePath);
            Assert.AreEqual(3, updatedReceipts.Count);
            Assert.AreEqual(newReceipt.Id, updatedReceipts[2].Id); // The new receipt should be last

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        public void AddReceipt_FileDoesNotExist_CreatesFileWithNewReceipt()
        {
            // Arrange
            string filePath = "non_existent_receipts.json";
            var order = new Order(new Table(1, 4), new List<Product>(), 100m, DateTime.Now);
            var newReceipt = new Receipt(order, ReceiptType.Restaurant);

            // Act
            _dataAccess.AddReceipt(newReceipt, filePath);

            // Assert
            Assert.IsTrue(File.Exists(filePath));
            var updatedReceipts = _dataAccess.ReadJson<Receipt>(filePath);
            Assert.AreEqual(1, updatedReceipts.Count);
            Assert.AreEqual(newReceipt.Id, updatedReceipts[0].Id);

            // Cleanup
            File.Delete(filePath);
        }

        [TestMethod]
        public void AddReceipt_FileIsEmpty_AddsReceiptAndUpdatesFile()
        {
            // Arrange
            string filePath = "empty_receipts.json";
            File.WriteAllText(filePath, "");

            var order = new Order(new Table(1, 4), new List<Product>(), 100m, DateTime.Now);
            var newReceipt = new Receipt(order, ReceiptType.Restaurant);

            // Act
            _dataAccess.AddReceipt(newReceipt, filePath);

            // Assert
            var updatedReceipts = _dataAccess.ReadJson<Receipt>(filePath);
            Assert.AreEqual(1, updatedReceipts.Count);
            Assert.AreEqual(newReceipt.Id, updatedReceipts[0].Id);

            // Cleanup
            File.Delete(filePath);
        }

        public class TestClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}