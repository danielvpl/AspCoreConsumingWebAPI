using Microsoft.EntityFrameworkCore;

namespace AspCoreModels
{
    public class ModelContext : DbContext
    {
        public DbSet<LedModel> LedModels { get; set; }

        public ModelContext(DbContextOptions<ModelContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LedModel>()
            .HasKey(o => new { o.id, o.gpio });
        }
    }       
}
