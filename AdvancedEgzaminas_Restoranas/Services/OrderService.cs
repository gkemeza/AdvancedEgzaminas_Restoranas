using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;

namespace AdvancedEgzaminas_Restoranas.Services
{
    // save to orders.csv
    public class OrderService : IOrderService
    {
        public OrderService(List<Order> orders, IDataAccess dataAccess, ITableService tableService, string filePath)
        {
            _orders = orders;
            _dataAccess = dataAccess;
            _tableService = tableService;
            //_filePath = filePath;
        }

        private List<Order> _orders;
        private readonly IDataAccess _dataAccess;
        private readonly ITableService _tableService;
        //private readonly string _filePath;

        public Order CreateOrder(int tableNumber, List<Product> products)
        {
            Table table = _tableService.GetTable(tableNumber);

            decimal totalPrice = 0;
            foreach (var product in products)
            {
                totalPrice = product.Price;
            }

            return new Order(table, products, totalPrice, DateTime.Now);
        }
    }
}
