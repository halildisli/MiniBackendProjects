using Microsoft.EntityFrameworkCore;
using StajApiDersi.Infrastructure.SeedData;
using StajApiDersi.Models.Concrete;

namespace StajApiDersi.Context
{
    public class StajApiDBContext:DbContext
    {
        public StajApiDBContext(DbContextOptions<StajApiDBContext> options):base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategorySeeding());
            base.OnModelCreating(modelBuilder);
        }
    }
}
