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

        public List<Table> GetAllTables() => _tables;

        public Table? GetTable(int tableNumber)
        {
            return _tables.FirstOrDefault(t => t.Number == tableNumber);
        }

        public bool AreFreeTables()
        {
            return _tables.Any(t => !t.IsOccupied);
        }

        public bool IsTableAvailable(int tableNumber)
        {
            return _tables.Any(t => t.Number == tableNumber && !t.IsOccupied);
        }

        public void FreeTable(int tableNumber)
        {
            var table = GetTable(tableNumber);
            if (table != null && table.IsOccupied)
            {
                table.IsOccupied = false;
            }
            else
            {
                Console.WriteLine("Stalas nerastas arba laisvas!");
            }
            SaveTables();
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
    }
}
