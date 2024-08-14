namespace AdvancedEgzaminas_Restoranas.Models
{
    public enum ReceiptType
    {
        Restaurant,
        Client
    }

    public class Receipt
    {
        public Order Order { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public ReceiptType Type { get; set; }

        public Receipt(Order order, ReceiptType type)
        {
            Order = order;
            Type = type;
        }

    }
}
