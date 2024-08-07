namespace AdvancedEgzaminas_Restoranas.Models
{
    public abstract class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public abstract string Type { get; }

        //public Product() { }

        //protected Product(string name, decimal price)
        //{
        //    Name = name;
        //    Price = price;
        //}
    }
}
