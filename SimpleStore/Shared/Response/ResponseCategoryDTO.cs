namespace SimpleStore.Shared.Response
{
    public class ResponseCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? IsDeleted { get; set; }
    }
}
