using Cafe.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Data
{
    public class CafeContext : DbContext
    {
        private string _connectionString;

        public DbSet<Category> Category { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemPrice> ItemPrice { get; set; }
        public DbSet<Server> Server { get; set; }
        public DbSet<TimeOfDay> TimeOfDay { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<CafeOrder> CafeOrder { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<ShoppingBag> ShoppingBag { get; set; }
        public DbSet<ShoppingBagItem> ShoppingBagItem { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<PaymentStatus> PaymentStatus { get; set; }
        public DbSet<ItemStatus> ItemStatus { get; set; }

        public CafeContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerID);

            modelBuilder.Entity<Customer>()
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Customer>(c => c.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
