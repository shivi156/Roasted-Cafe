using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("money");
                entity.ToTable("tbl_menu");
            });
                
            base.OnModelCreating(modelBuilder);
        }
    }

    
}
