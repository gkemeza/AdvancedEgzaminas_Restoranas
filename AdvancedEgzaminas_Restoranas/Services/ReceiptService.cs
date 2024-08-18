using AdvancedEgzaminas_Restoranas.DataAccess;
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
            var chosenType = PromptForReceiptType();
            _userInterface.PrintReceipts(receipts, chosenType);
            _userInterface.DisplayMessageAndWait(string.Empty);
        }

        private ReceiptType PromptForReceiptType()
        {
            var receiptTypes = Enum.GetValues(typeof(ReceiptType)).Cast<ReceiptType>().ToList();

            while (true)
            {
                Console.Clear();
                PrintReceiptTypes(receiptTypes);

                Console.WriteLine("\nEnter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int number))
                {
                    _userInterface.DisplayMessageAndWait("Enter a whole number!");
                    continue;
                }

                if (number < 1 || number > 2)
                {
                    _userInterface.DisplayMessageAndWait($"Receipt type {number} doesn't exist!");
                    continue;
                }

                return receiptTypes[number - 1];
            }
        }

        private void PrintReceiptTypes(List<ReceiptType> receiptTypes)
        {
            Console.WriteLine("Receipt types:");
            for (int i = 0; i < receiptTypes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {receiptTypes[i]}");
            }
        }

        public void SendEmail()
        {
            if (IsEmailSendNeeded())
            {
                Console.WriteLine("Email was sent.");
            }
        }

        private bool IsEmailSendNeeded()
        {

            string choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Send receipt(s) to email? (Y/N)");
                choice = Console.ReadLine();
            }
            while (!choice.Equals("y", StringComparison.OrdinalIgnoreCase) &&
                   !choice.Equals("n", StringComparison.OrdinalIgnoreCase));

            return choice.Equals("y", StringComparison.OrdinalIgnoreCase);
        }
    }
}
