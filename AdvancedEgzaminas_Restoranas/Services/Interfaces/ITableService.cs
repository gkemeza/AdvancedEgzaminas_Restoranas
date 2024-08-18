using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface ITableService
    {
        Table GetTable(int tableNumber);
        bool IsTableAvailable(int tableNumber);
        bool FreeTable(int tableNumber);
        bool OccupyTable(int tableNumber);
        void UpdateTablesInFile();
        int ChooseTable();
        void SeedTables();
        void PrintTables();
    }
}
