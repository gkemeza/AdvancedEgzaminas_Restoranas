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
            Console.WriteLine("q. Go back");
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
    }
}
