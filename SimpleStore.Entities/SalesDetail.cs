namespace SimpleStore.Entities
{
    public class SalesDetail : EntityBase
    {
        public int Correlative { get; set; } = default!;
        public decimal UnitPrice { get; set; } = default!;
        public int Quantity { get; set; } = default!;
        public decimal Total { get; set; } = default!;

        public int SaleId { get; set; }
        public Sale Sale { get; set; } = default!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = default!;
    }
}
