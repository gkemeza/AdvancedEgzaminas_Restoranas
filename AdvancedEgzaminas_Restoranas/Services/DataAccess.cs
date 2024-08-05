using AdvancedEgzaminas_Restoranas.Services.Interfaces;

namespace AdvancedEgzaminas_Restoranas.Services
{
    public class DataAccess : IDataAccess
    {
        public List<T> ReadCsv<T>(string filePath)
        {
            throw new NotImplementedException();
        }

        public void WriteCsv<T>(string filePath, List<T> data)
        {
            throw new NotImplementedException();
        }
    }
}
