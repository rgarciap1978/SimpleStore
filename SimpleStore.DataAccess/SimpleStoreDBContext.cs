using Microsoft.EntityFrameworkCore;
using SimpleStore.DataAccess.Seeds;
using SimpleStore.Entities;

namespace SimpleStore.DataAccess
{
    public class SimpleStoreDBContext : DbContext
    {
        public SimpleStoreDBContext(DbContextOptions<SimpleStoreDBContext> options)  
            : base(options) 
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategorySeed());
        }
    }
}