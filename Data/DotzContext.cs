using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Web.API.Dotz.Entities;

namespace Web.API.Dotz.Data
{
    public class DotzContext : DbContext
    {
        public DotzContext(DbContextOptions<DotzContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<UserDotz> UserDotz { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserAddress>()
               .HasKey(u => new { u.Id });

            builder.Entity<Order>()
           .HasKey(o => new { o.Id });

            builder.Entity<OrderItems>()
           .HasKey(o => new { o.Id });

            // carga inicial
            builder.Entity<User>()
                  .HasData(new List<User>(){
                    new User(1,"admin", "", "admin", "123", Role.Admin, "", null),
                    new User(2,"user", "", "user", "123", Role.User, "", null ),
                  });


            builder.Entity<Product>()
             .HasData(new List<Product>(){
                    new Product(1, "Jogo do mico", 2.814),
                    new Product(2, "Pneu aro 14", 21.380),
                    new Product(3, "Balança Digital", 5.577),
                    new Product(4, "Banheira de babá", 21.856),
                    new Product(5, "Panela de pressao", 6.748),
             });

            builder.Entity<UserDotz>()
        .HasData(new List<UserDotz>(){
                    new UserDotz(1,  5522.814, 1),
                    new UserDotz(2, 1221.380, 2)
        });

        }

    }


    public class DotzContextFactory : IDesignTimeDbContextFactory<DotzContext>
    {
        public DotzContextFactory()
        {

        }

        public DotzContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DotzContext>();
            optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=DotzDB;User=root;Password=000000;");

            return new DotzContext(optionsBuilder.Options);
        }
    }
}