using AdvancedEgzaminas_Restoranas.DataAccess;
using AdvancedEgzaminas_Restoranas.Enums;
using AdvancedEgzaminas_Restoranas.Models;
using AdvancedEgzaminas_Restoranas.Services.Interfaces;
using AdvancedEgzaminas_Restoranas.UI;

namespace AdvancedEgzaminas_Restoranas.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly IDataAccess _dataAccess;
        private readonly UserInterface _userInterface;
        private readonly string _receiptsFilePath;

        public ReceiptService(IDataAccess dataAccess, UserInterface userInterface, string filePath)
        {
            _dataAccess = dataAccess;
            _userInterface = userInterface;
            _receiptsFilePath = filePath;
        }

        public Receipt HandleRestaurantReceipt(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var receipt = new Receipt(order, ReceiptType.Restaurant);
            _dataAccess.AddReceipt(receipt, _receiptsFilePath);
            return receipt;
        }

        public Receipt HandleClientReceipt(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var receipt = new Receipt(order, ReceiptType.Client);
            _dataAccess.AddReceipt(receipt, _receiptsFilePath);
            return receipt;
        }

        public List<Receipt> GetAllReceipts()
        {
            return _dataAccess.ReadJson<Receipt>(_receiptsFilePath);
        }

        public void ShowReceipts()
        {
            Console.Clear();
            var receipts = GetAllReceipts();
            var chosenType = _userInterface.PromptForReceiptType();
            _userInterface.PrintReceipts(receipts, chosenType);
            _userInterface.DisplayMessageAndWait(string.Empty);
        }

    }
}
