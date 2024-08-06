using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using CsvHelper;
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

            using (var reader = new StreamReader(filePath))

            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<T>().ToList();
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
