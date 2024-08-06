using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;

namespace AdvancedEgzaminas_Restoranas.Services
{
    public class TableService : ITableService
    {
        private List<Table> _tables;
        private readonly IDataAccess _dataAccess;
        private readonly string _filePath;

        public bool AreFreeTables()
        {
            throw new NotImplementedException();
        }

        public void FreeTable(int tableNumber)
        {
            throw new NotImplementedException();
        }

        public List<Table> GetAllTables()
        {
            throw new NotImplementedException();
        }

        public Table GetTable(int tableNumber)
        {
            throw new NotImplementedException();
        }

        public bool IsTableAvailable(int tableNumber)
        {
            throw new NotImplementedException();
        }

        public void OccupyTable(int tableNumber)
        {
            throw new NotImplementedException();
        }
    }
}
