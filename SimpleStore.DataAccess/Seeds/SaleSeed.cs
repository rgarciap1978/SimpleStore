using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStore.Entities;

namespace SimpleStore.DataAccess.Seeds
{
    internal class SaleSeed : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder
                .HasKey(pk => pk.Id);

            builder
                .Property(f => f.DateSale)
                .HasDefaultValueSql("getdate()");

            builder
                .Property(f => f.CreatedDate)
                .HasDefaultValueSql("getdate()");
        }
    }
}
