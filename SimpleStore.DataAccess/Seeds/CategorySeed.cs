using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStore.Entities;

namespace SimpleStore.DataAccess.Seeds
{
    public class CategorySeed : IEntityTypeConfiguration<Category>

    {
        public void Configure(EntityTypeBuilder<Category> builder)
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
                new Category() { Id = 1, Name = "Categoria Base", CreatedDate = DateTime.Now, IsDeleted = false }
                );
        }
    }
}
