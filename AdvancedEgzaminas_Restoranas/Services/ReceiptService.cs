using AdvancedEgzaminas_Restoranas.DataAccess;
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
            var receipt = new Receipt(order, ReceiptType.Restaurant);
            AddReceipt(receipt);
            return receipt;
        }

        public Receipt HandleClientReceipt(Order order)
        {
            var receipt = new Receipt(order, ReceiptType.Client);
            AddReceipt(receipt);
            return receipt;
        }

        public bool IsClientReceiptNeeded()
        {
            string choice;
            do
            {
                Console.WriteLine("Print client receipt? (Y/N)");
                choice = Console.ReadLine();
            }
            while (choice.ToLower() != "y" && choice.ToLower() != "n");

            if (choice == null)
            {
                throw new NullReferenceException();
            }
            if (choice.ToLower() == "y")
            {
                return true;
            }

            return false;
        }

        private void AddReceipt(Receipt receipt)
        {
            var receipts = _dataAccess.ReadJson<Receipt>(_receiptsFilePath);
            receipts.Add(receipt);
            _dataAccess.WriteJson<Receipt>(_receiptsFilePath, receipts);
        }

        public List<Receipt> GetAllReceipts()
        {
            return _dataAccess.ReadJson<Receipt>(_receiptsFilePath);
        }

        public void PrintReceipts(List<Receipt> receipts, ReceiptType receiptType)
        {
            Console.Clear();
            Random rand = new Random();
            var filteredReceipts = receipts.Where(r => r.Type == receiptType).ToList();

            if (filteredReceipts.Count == 0)
            {
                Console.WriteLine("No receipts found.");
                return;
            }
            Console.WriteLine($"***** {receiptType} Receipts *****\n");
            foreach (Receipt receipt in filteredReceipts)
            {
                Console.WriteLine($"{receiptType} receipt:");
                Console.WriteLine($"{rand.Next(200)} - Vardenis Pavardenis\n");

                Console.WriteLine("Check | Tbl | Opened | Amt Due");
                Console.WriteLine(new string('-', 36));

                Console.WriteLine($"{rand.Next(100, 1000)} | {receipt.Order.Table.Number} | " +
                    $"{receipt.Order.OrderTime:g} | {receipt.Order.TotalAmount} Eur\n");

                Console.WriteLine($"Receipt Num. {receipt.Id}");
                Console.WriteLine();
                Console.WriteLine(new string('-', 50));
                Console.WriteLine();
            }
        }

        public void ShowReceipts()
        {
            Console.Clear();
            var receipts = GetAllReceipts();
            PromptAndPrintReceipts(receipts);

            Console.WriteLine("\nPress any key to go back.");
            Console.ReadKey();
        }

        private void PromptAndPrintReceipts(List<Receipt> allReceipts)
        {
            Console.WriteLine("Choose receipts type:");
            foreach (ReceiptType type in Enum.GetValues(typeof(ReceiptType)))
            {
                Console.WriteLine($"- {type}");
            }

            while (true)
            {
                Console.Write("\nEnter your choice: ");
                string userInput = Console.ReadLine();

                if (Enum.TryParse(userInput, true, out ReceiptType chosenType))
                {
                    PrintReceipts(allReceipts, chosenType);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid receipt type!");
                }
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
                Console.WriteLine("Send receipt(s) to email? (Y/N)");
                choice = Console.ReadLine();
            }
            while (choice.ToLower() != "y" && choice.ToLower() != "n");

            if (choice == null)
            {
                throw new NullReferenceException();
            }
            if (choice.ToLower() == "y")
            {
                return true;
            }

            return false;
        }
    }
}
