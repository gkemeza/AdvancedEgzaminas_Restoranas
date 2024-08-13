﻿using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface ITableService
    {
        Table GetTable(int tableNumber);
        bool AreFreeTables();
        bool IsTableAvailable(int tableNumber);
        bool IsTableFree(int tableNumber);
        void FreeTable(int tableNumber);
        void OccupyTable(int tableNumber);
        void UpdateTablesInFile();
        int ChooseTable();
        void SeedTables();
        void PrintTables();
    }
}
