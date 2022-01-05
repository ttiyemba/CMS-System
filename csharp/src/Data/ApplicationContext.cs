using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace src.Data
{
    public class ApplicationContext : DbContext

    {
        public ApplicationContext (DbContextOptions<ApplicationContext>options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoveableEntity>()
                .HasOne(moveable => moveable.PlottableEntity)
                .WithOne(plottable => plottable.MoveableEntity)
                .HasForeignKey<MoveableEntity>(moveable => moveable.PlottableEntityID)
                .OnDelete(DeleteBehavior.Cascade);


        }
        public DbSet<MoveableEntity> MoveableEntities { get; set; }
        public DbSet<PlottableEntity> PlottableEntities { get; set; }
        public DbSet<NavigationPlatform> Platform { get; set; }

    }
}
