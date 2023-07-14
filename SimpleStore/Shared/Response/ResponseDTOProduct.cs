namespace SimpleStore.Shared.Response
{
    public class ResponseDTOProduct
    {
        public int Id { get; set; }
        public string SkuCode { get; set; } = default!;
        public string Name { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public string? Image { get; set; } = default!;
        public string? Comment { get; set; } = default!;
        public bool Status { get; set; }
        public string StringStatus => Status ? "Activo" : "Inactivo";

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
    }
}
