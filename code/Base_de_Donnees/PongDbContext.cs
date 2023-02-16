using Microsoft.EntityFrameworkCore;

namespace Base_de_Donnees
{
    public class PongDbContext : DbContext
    {

        public PongDbContext() { }
        public PongDbContext(DbContextOptions<PongDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source=PongBdd.db");
            }
        }
    }
}
