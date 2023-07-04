namespace SimpleStore.Entities
{
    public class Sale
    {
        public DateTime SaleDate { get; set; }
        public string DocumentNumber { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal SubTotal { get; set; }
        public decimal Taxes { get; set; }
        public decimal Total { get; set; }
        public bool DocumentState { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = default!;

        public Sale()
        {
            DocumentState = true;
        }

    }
}
