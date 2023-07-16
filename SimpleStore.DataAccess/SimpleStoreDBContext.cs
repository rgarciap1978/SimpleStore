using Microsoft.EntityFrameworkCore;
using SimpleStore.DataAccess.Seeds;
using SimpleStore.Entities;
using System.Reflection;

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
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}