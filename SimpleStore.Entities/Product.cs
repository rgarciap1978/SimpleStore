namespace SimpleStore.Entities
{
    public class Product : EntityBase
    {
        public string SkuCode { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Image { get; set; } = default!;
        public string? Comment { get; set; } = default!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;
    }
}
