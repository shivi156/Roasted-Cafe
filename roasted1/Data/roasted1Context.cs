using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using roasted1.Models;

namespace roasted1.Data
{
    public class roasted1Context : IdentityDbContext<ApplicationUser>
    {

        public roasted1Context (DbContextOptions<roasted1Context> options)
            : base(options)
        {
            
            
        }

        public DbSet<roasted1.Models.Menu> Menu { get; set; } = default!;
        public DbSet<CheckoutCustomer> CheckoutCustomers { get; set; }	
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        [NotMapped]
        public DbSet<CheckoutItems> CheckoutItems { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("money");
                entity.ToTable("tbl_menu");
            });
                
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BasketItem>().HasKey(t => new { t.StockID, t.BasketID });
            modelBuilder.Entity<OrderItem>().HasKey(t => new { t.StockID, t.OrderNo });
        }
    }

  
}
