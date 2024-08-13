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

        public List<Receipt> GetReceipts()
        {
            return _dataAccess.ReadJson<Receipt>(_receiptsFilePath);
        }

        public void PrintReceipts(List<Receipt> receipts)
        {
            Random rand = new Random();
            if (receipts.Count == 0)
            {
                Console.WriteLine("No receipts found.");
                return;
            }
            Console.WriteLine("***** Receipts *****\n");
            foreach (Receipt receipt in receipts)
            {
                Console.WriteLine($"{receipt.Type} receipt");
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
    }
}
