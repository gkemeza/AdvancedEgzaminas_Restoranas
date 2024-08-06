using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;

namespace AdvancedEgzaminas_Restoranas.Services
{
    public class OrderService : IOrderService
    {
        private List<Table> _orders;
        private readonly IDataAccess _dataAccess;
        private readonly string _filePath;

        public Order CreateOrder(int tableNumber, List<Product> products)
        {
            throw new NotImplementedException();
        }
    }
}
