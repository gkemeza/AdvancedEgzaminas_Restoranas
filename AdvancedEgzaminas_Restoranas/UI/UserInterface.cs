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

        public Product ChooseProduct(List<Product> products)
        {
            string chosenName = PromptForProductName();

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

        private string PromptForProductName()
        {
            Console.WriteLine("Enter product name:");
            return Console.ReadLine();
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

    }
}
