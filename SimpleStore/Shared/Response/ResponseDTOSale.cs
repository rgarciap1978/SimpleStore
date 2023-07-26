namespace SimpleStore.Shared.Response
{
    public record ResponseDTOSale
    {
        public int Id { get; set; }
        public string VoucherNumber { get; set; } = default!;
        public DateTime DateSale { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
        public short Quantity { get; set; } = default!;
        public decimal Total { get; set; } = default!;
    }
}
