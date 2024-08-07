namespace AdvancedEgzaminas_Restoranas.Models
{
    public class Order
    {
        public Guid ID { get; set; }
        public Table Table { get; set; }
        public List<Product> Products { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderTime { get; set; }

        public Order()
        {

        }

        public Order(Table table, List<Product> products, decimal totalAmount, DateTime orderTime)
        {
            ID = Guid.NewGuid();
            Table = table;
            Products = products;
            TotalAmount = totalAmount;
            OrderTime = orderTime;
        }
    }
}
