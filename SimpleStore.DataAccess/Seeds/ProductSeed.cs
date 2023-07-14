using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStore.Entities;

namespace SimpleStore.DataAccess.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>

    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasKey(pk => pk.Id);

            builder
                .Property(f => f.Name)
                .HasMaxLength(150);

            builder
                .Property(f => f.CreatedDate)
                .HasDefaultValueSql("getdate()");

            builder
                .HasData(
                new Product() { Id = 1, SkuCode = "13101978", Name = "Producto Base", UnitPrice = 44, Comment = "Producto de inicialización de la base de datos", CategoryId = 1, CreatedDate = DateTime.Now, Status = true }
                );
        }
    }
}
