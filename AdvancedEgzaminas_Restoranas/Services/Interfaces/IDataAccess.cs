﻿namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface IDataAccess
    {
        List<T> ReadCsv<T>(string filePath);
        void WriteCsv<T>(string filePath, List<T> data);
    }
}