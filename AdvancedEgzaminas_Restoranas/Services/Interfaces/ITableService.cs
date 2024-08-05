using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface ITableService
    {
        List<Table> GetAllTables();
        Table GetTable(int tableNumber);
        bool AreFreeTables();
        bool IsTableAvailable(int tableNumber);
        void OccupyTable(int tableNumber);
        void FreeTable(int tableNumber);
    }
}
