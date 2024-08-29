namespace AdvancedEgzaminas_Restoranas.Models
{
    public class Food : Product
    {
        public override string Type => "Food";

        public Food() { }

        public Food(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

    }
}
