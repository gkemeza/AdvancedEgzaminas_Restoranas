﻿using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace AdvancedEgzaminas_Restoranas.Services
{
    public class DataAccess : IDataAccess
    {
        public List<T> ReadCsv<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            var conf = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim,
                MissingFieldFound = null
            };

            using (var reader = new StreamReader(filePath))

            using (var csv = new CsvReader(reader, conf))
            {
                try
                {
                    return csv.GetRecords<T>().ToList();
                }
                catch (ReaderException ex)
                {
                    Console.WriteLine($"Error reading CSV: {ex.Message}");
                    Console.WriteLine($"Error occurred on row {ex.Context.Parser.Row}");
                    throw;
                }
            }
        }

        public void WriteCsv<T>(string filePath, List<T> data)
        {
            using (var writer = new StreamWriter(filePath))

            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(data);
            }
        }
    }
}
