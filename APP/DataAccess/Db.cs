using Microsoft.EntityFrameworkCore;

namespace APP.DataAccess
{
    public class Db : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<ProductStore> ProductStores { get; set; }

        public Db(DbContextOptions options) : base(options)
        {
        }

        // ilişkiler konfigüre edilebilir
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
