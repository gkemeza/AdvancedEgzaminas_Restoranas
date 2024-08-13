namespace AdvancedEgzaminas_Restoranas.DataAccess
{
    public interface IDataAccess
    {
        List<T> ReadCsv<T>(string filePath);
        void WriteCsv<T>(string filePath, List<T> data);

        public List<T> ReadJson<T>(string filePath);
        void WriteJson<T>(string filePath, List<T> data);
    }
}
