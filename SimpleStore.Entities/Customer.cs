namespace SimpleStore.Entities
{
    public class Customer : BaseEntity
    {
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
    }
}
