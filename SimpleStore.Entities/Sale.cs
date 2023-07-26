namespace SimpleStore.Entities
{
    public class Sale : BaseEntity
    {
        public DateTime DateSale { get; set; }
        public string Correlative { get; set; } = default!;
        public decimal Total { get; set; }
        public short Quantity { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;
        public DateTime SaleDate { get; set; }
    }
}
