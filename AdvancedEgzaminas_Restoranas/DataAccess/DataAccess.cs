using CsvHelper;
using System.Globalization;
using System.Text.Json;

namespace AdvancedEgzaminas_Restoranas.DataAccess
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
                try
                {
                    var temp = csv.GetRecords<T>();
                    return temp.ToList();
                }
                catch (ReaderException ex)
                {
                    Console.WriteLine($"Error reading CSV: {ex.Message}");
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

        public List<T> ReadJson<T>(string filePath)
        {
            var items = new List<T>();
            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            var order = JsonSerializer.Deserialize<T>(line, GetJsonSerializerOptions());
                            items.Add(order);
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"File not found: {e}");
            }
            catch (JsonException e)
            {
                Console.WriteLine($"Deserialization error: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
            return items;
        }

        public void WriteJson<T>(string filePath, List<T> data)
        {
            var lines = data.Select(item => JsonSerializer.Serialize(item));
            File.WriteAllLines(filePath, lines);
        }

        private JsonSerializerOptions GetJsonSerializerOptions()
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new ProductConverter() }
            };
            return options;
        }
    }
}
