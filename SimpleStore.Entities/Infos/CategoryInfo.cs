namespace SimpleStore.Entities.Infos
{
    public class CategoryInfo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string IsDeleted { get; set; } = default!;
    }
}
