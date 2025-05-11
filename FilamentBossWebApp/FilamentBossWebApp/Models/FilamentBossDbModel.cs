using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FilamentBossWebApp.Models
{
    public partial class FilamentBossDbModel : DbContext
    {
        public FilamentBossDbModel()
            : base("name=FilamentBossDbModel")
        {
        }
        public DbSet<ManagerRole> ManagerRoles{ get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Cart> Carts { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.Supplier_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.Category_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.Brand_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasRequired(c => c.Supplier)
                .WithMany(s => s.Categories)
                .HasForeignKey(c => c.Supplier_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Brand>()
                .HasRequired(b => b.Supplier)
                .WithMany(s => s.Brands)
                .HasForeignKey(b => b.Supplier_ID)
                .WillCascadeOnDelete(false);
        }
    }
}
