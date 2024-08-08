using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;

namespace AdvancedEgzaminas_Restoranas.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IDataAccess _dataAccess;
        private readonly string _receiptsFilePath;

        public ReceiptService(IDataAccess dataAccess, string filePath)
        {
            _dataAccess = dataAccess;
            _receiptsFilePath = filePath;
        }

        public Receipt HandleRestaurantReceipt(Order order)
        {
            var receipt = new Receipt(order, "Restaurant");
            AddReceipt(receipt);
            return receipt;
        }

        private void AddReceipt(Receipt receipt)
        {
            var receipts = _dataAccess.ReadJson<Receipt>(_receiptsFilePath);
            receipts.Add(receipt);
            _dataAccess.WriteJson<Receipt>(_receiptsFilePath, receipts);
        }
    }
}
