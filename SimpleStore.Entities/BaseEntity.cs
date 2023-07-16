namespace SimpleStore.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public BaseEntity()
        {
            Status = true;
            IsDeleted = false;
            CreatedDate = null;
            ModifiedDate = null;
            DeletedDate = null;
        }
    }
}
