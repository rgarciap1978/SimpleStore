namespace SimpleStore.Entities
{
    public class Product : EntityBase
    {
        public string SkuCode { get; set; } = default!;
        public string Name { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public string? Image { get; set; } = default!;
        public string? Comment { get; set; } = default!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
