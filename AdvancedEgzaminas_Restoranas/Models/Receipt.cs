namespace AdvancedEgzaminas_Restoranas.Models
{
    public class Receipt
    {
        public Order Order { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; }

        public Receipt(Order order, string type)
        {
            Order = order;
            Type = type;
        }

    }
}
