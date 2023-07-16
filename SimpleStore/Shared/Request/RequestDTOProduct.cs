namespace SimpleStore.Shared.Request
{
    public class RequestDTOProduct
    {
        public string SkuCode { get; set; } = default!;
        public string Name { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public string? Image { get; set; } = default!;
        public string? Base64Image { get; set; }
        public string? FileName { get; set; }
        public string? Comment { get; set; } = default!;
        public bool Status { get; set; }

        public int CategoryId { get; set; }
    }
}
