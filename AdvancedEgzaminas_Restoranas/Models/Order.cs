namespace AdvancedEgzaminas_Restoranas.Models
{
    public class Order
    {
        public int ID { get; set; }
        public Table Table { get; set; }
        public List<Product> Products { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderTime { get; set; }
    }
}
