using ColorSwatch.Domain;
using Microsoft.EntityFrameworkCore;

namespace SwtchApi.Data
{
    public class SwatchDbContext : DbContext
    {
        public SwatchDbContext(DbContextOptions<SwatchDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ColorSwatcher>().HasKey(c => c.Id);
            modelBuilder.Entity<ColorSwatcher>().Property(c => c.HexColor).IsRequired();
        }

        public DbSet<ColorSwatcher> Colors { get; set; }
    }
}