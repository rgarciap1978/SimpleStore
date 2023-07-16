namespace SimpleStore.Entities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int Phone { get; set; } = default!;
        public int Edad { get; set; } = default!;
    }
}
