using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;

namespace AdvancedEgzaminas_Restoranas.Services
{
    public class TableService : ITableService
    {
        private List<Table> _tables;
        private readonly IDataAccess _dataAccess;
        private readonly string _filePath;

        public TableService(IDataAccess dataAccess, string filePath)
        {
            _dataAccess = dataAccess;
            _filePath = filePath;
            _tables = _dataAccess.ReadCsv<Table>(_filePath);
        }

        public int ChooseTable()
        {
            Console.WriteLine("Enter table number (1-10):");
            // TODO: validate input
            return int.Parse(Console.ReadLine());
        }

        public List<Table> GetAllTables() => _tables;

        public Table? GetTable(int tableNumber)
        {
            _tables = _dataAccess.ReadCsv<Table>(_filePath);
            return _tables.FirstOrDefault(t => t.Number == tableNumber);
        }

        public bool AreFreeTables()
        {
            _tables = _dataAccess.ReadCsv<Table>(_filePath);
            return _tables.Any(t => !t.IsOccupied);
        }

        public bool IsTableAvailable(int tableNumber)
        {
            _tables = _dataAccess.ReadCsv<Table>(_filePath);
            return _tables.Any(t => t.Number == tableNumber && !t.IsOccupied);
        }

        public bool IsTableFree(int tableNumber)
        {
            if (_tables.Any(t => t.Number == tableNumber))
            {
                var table = GetTable(tableNumber);
                return !table.IsOccupied;
            }
            else { return false; }
        }

        public void FreeTable(int tableNumber)
        {
            var table = GetTable(tableNumber);
            if (table != null && table.IsOccupied)
            {
                table.IsOccupied = false;
                SaveTables();
            }
            else
            {
                Console.WriteLine("Stalas nerastas arba laisvas!");
            }
        }

        public void OccupyTable(int tableNumber)
        {
            var table = GetTable(tableNumber);
            if (table != null && !table.IsOccupied)
            {
                table.IsOccupied = true;
            }
            else
            {
                Console.WriteLine("Stalas nerastas arba uzimtas!");
            }
            SaveTables();
        }

        public void SaveTables() => _dataAccess.WriteCsv<Table>(_filePath, _tables);

        public void SeedTables()
        {
            var tables = new List<Table>()
            {
                new Table (1, 2),
                new Table (2, 2),
                new Table (3, 2),
                new Table (4, 4),
                new Table (5, 4),
                new Table (6, 2),
                new Table (7, 12),
                new Table (8, 2),
                new Table (9, 2),
                new Table (10, 2),
            };

            _dataAccess.WriteCsv(_filePath, tables);
        }
    }
}
