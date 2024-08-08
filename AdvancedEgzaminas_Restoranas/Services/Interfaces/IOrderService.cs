using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(int tableNumber, List<Product> products);
        void HandleOrderMenu(int tableNumber);
        void EndOrder(int tableNumber);
        Order? GetOrder(int tableNumber);
        List<Order> GetOrders();
    }
}
