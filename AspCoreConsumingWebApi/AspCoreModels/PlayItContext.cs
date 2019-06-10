using Microsoft.EntityFrameworkCore;

namespace AspCoreModels
{
    public class PlayItContext : DbContext
    {
        public DbSet<LeituraPreviewModel> LeituraPreviews { get; set; }

        public PlayItContext(DbContextOptions<PlayItContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LeituraPreviewModel>()
            .HasKey(o => new { o.ltp_codigo });
        }
    }
}
