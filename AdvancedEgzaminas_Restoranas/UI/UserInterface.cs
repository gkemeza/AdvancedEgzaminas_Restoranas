using AdvancedEgzaminas_Restoranas.Models;

namespace AdvancedEgzaminas_Restoranas.UI
{
    public class UserInterface
    {
        public void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("***** Winestone *****");
            Console.WriteLine("1. Begin table");
            Console.WriteLine("2. Open tables");
            Console.WriteLine("3. Receipts");
            Console.WriteLine("4. View Tables");
            Console.WriteLine("q. Exit");
        }

        public void DisplayOrderMenu()
        {
            Console.Clear();
            Console.WriteLine("***** Order *****");
            Console.WriteLine("1. Add product");
            Console.WriteLine("2. Service");
        }

        public int PromptForTableNumber()
        {
            int result = 0;
            Console.WriteLine("Choose table number to finish order:");

            if (int.TryParse(Console.ReadLine(), out int tableNumber))
            {
                if (tableNumber > 0 || tableNumber <= 10)
                {
                    result = tableNumber;
                }
            }
            return result;
        }

        public void PrintTableStatus(Table table)
        {
            Console.Write($"{table.Number}. {table.Seats} seats - ");
            if (table.IsOccupied)
            {
                Console.WriteLine($"(taken)");
            }
            else
            {
                Console.WriteLine($"(free)");
            }
        }

        public void DisplayMessageAndWait(string message)
        {
            if (message != string.Empty)
            {
                Console.WriteLine(message);
            }
            Console.WriteLine("\nPress 'Enter' to continue...");
            Console.ReadLine();
        }

        public void DisplayProductsMenu(List<Product> products)
        {
            if (products.Count == 0)
            {
                Console.WriteLine("No food items found.");
                return;
            }

            Console.Clear();
            Console.WriteLine("***** Menu *****");
            Console.WriteLine("ID | Name | Price");
            Console.WriteLine("-----------------");

            int i = 1;
            foreach (var item in products)
            {
                Console.WriteLine($"{i++} | {item.Name} | ${item.Price:F2}");
            }
        }

        public Product ChooseProduct(List<Product> products, string chosenName)
        {
            Product? product = null;
            if (products.Any(p => string.Equals(p.Name, chosenName, StringComparison.OrdinalIgnoreCase)))
            {
                product = products.FirstOrDefault(p => string.Equals(p.Name, chosenName, StringComparison.OrdinalIgnoreCase));
                Console.WriteLine($"{product.Name} was addded to the order");
            }
            else
            {
                Console.WriteLine("Wrong name!");
            }
            return product;
        }

        public string PromptForProductName()
        {
            Console.WriteLine("Enter product name:");
            return Console.ReadLine();
        }

        public void PrintOrders(List<Order> orders)
        {
            if (orders.Count == 0)
            {
                Console.WriteLine("No open tables found.");
                return;
            }
            Console.WriteLine("***** Open Tables *****\n");
            foreach (Order order in orders)
            {
                Console.WriteLine($"Table Number: {order.Table.Number}");
                Console.WriteLine($"Seats: {order.Table.Seats}");
                Console.WriteLine($"Order Time: {order.OrderTime}");
                Console.WriteLine("Products:");
                foreach (var product in order.Products)
                {
                    Console.WriteLine($"- {product.Name} ({product.Type}): {product.Price} Eur");
                }
                Console.WriteLine($"Total Amount: {order.TotalAmount} Eur");
                Console.WriteLine(new string('-', 40));
            }
        }

        public bool IsClientReceiptNeeded()
        {
            string choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Print client receipt? (Y/N)");
                choice = Console.ReadLine();
            }
            while (!choice.Equals("y", StringComparison.OrdinalIgnoreCase) &&
                    !choice.Equals("n", StringComparison.OrdinalIgnoreCase));

            return choice.Equals("y", StringComparison.OrdinalIgnoreCase);
        }

        public ReceiptType PromptForReceiptType()
        {
            var receiptTypes = Enum.GetValues(typeof(ReceiptType)).Cast<ReceiptType>().ToList();

            while (true)
            {
                Console.Clear();
                PrintReceiptTypes(receiptTypes);

                Console.WriteLine("\nEnter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out int number))
                {
                    DisplayMessageAndWait("Enter a whole number!");
                    continue;
                }

                if (number < 1 || number > 2)
                {
                    DisplayMessageAndWait($"Receipt type {number} doesn't exist!");
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
                Console.WriteLine(new string('-', 50));
                Console.WriteLine();
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
