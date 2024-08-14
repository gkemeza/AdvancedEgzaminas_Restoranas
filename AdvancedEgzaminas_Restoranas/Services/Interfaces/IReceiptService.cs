using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.Services.Interfaces
{
    public interface IReceiptService
    {
        Receipt HandleRestaurantReceipt(Order order);
        Receipt HandleClientReceipt(Order order);
        bool IsClientReceiptNeeded();
        List<Receipt> GetAllReceipts();
        void PrintReceipts(List<Receipt> receipts, ReceiptType receiptType);
        void ShowReceipts();
        void SendEmail();
    }
}
