using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStore.Entities;

namespace SimpleStore.DataAccess.Seeds
{
    public class CustomerSeed : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .HasKey(pk => pk.Id);

            builder
                .Property(f => f.FullName)
                .HasMaxLength(300);

            builder
                .Property(f => f.CreatedDate)
                .HasDefaultValueSql("getdate()");
        }
    }
}
