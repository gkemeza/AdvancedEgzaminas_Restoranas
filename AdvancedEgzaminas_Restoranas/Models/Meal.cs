namespace AdvancedEgzaminas_Restoranas.Models
{
    public class Meal : Product
    {
        public override string Type => "Meal";

        public Meal(string name, decimal price) : base(name, price)
        {
        }
    }
}
