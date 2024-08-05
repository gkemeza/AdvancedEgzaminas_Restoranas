using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface IReceiptService
    {
        List<Receipt> GenerateReceipts(Order order);
        void SaveRestaurantReceipt(Receipt receipt);
        // string GenerateCustomerReceipt(Order order)
        // string GenerateRestaurantReceipt(Order order)
    }
}
