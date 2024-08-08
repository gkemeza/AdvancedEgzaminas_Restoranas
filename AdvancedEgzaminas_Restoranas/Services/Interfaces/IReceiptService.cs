using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface IReceiptService
    {
        Receipt HandleRestaurantReceipt(Order order);
    }
}
