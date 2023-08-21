using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleStore.DataAccess.Seeds;
using SimpleStore.Entities;
using System.Reflection;

namespace SimpleStore.DataAccess
{
    public class SimpleStoreDBContext : IdentityDbContext<SimpleStoreUserIdentity>
    {
        public SimpleStoreDBContext(DbContextOptions<SimpleStoreDBContext> options)  
            : base(options) 
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SimpleStoreUserIdentity>(e => {
                e.ToTable(name: "User");
            });
            modelBuilder.Entity<IdentityRole>(e => {
                e.ToTable(name: "Role");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(e => {
                e.ToTable(name: "UserRole");
            });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}