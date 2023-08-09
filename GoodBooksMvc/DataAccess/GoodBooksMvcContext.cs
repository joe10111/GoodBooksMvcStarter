using GoodBooksMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodBooksMvc.DataAccess
{
    public class GoodBooksMvcContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public GoodBooksMvcContext(DbContextOptions<GoodBooksMvcContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Dune", Description = "Sand Monsters" },
                new Book { Id = 2, Title = "Wool", Description = "Human Monsters" },
                new Book { Id = 3, Title = "It", Description = "Monster Monsters"});
        }
    }
}
