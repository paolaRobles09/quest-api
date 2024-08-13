using Microsoft.EntityFrameworkCore;

namespace Quest.Infrastructure.Data.EntityFramework
{
    public class QuestDbContext : DbContext
    {
        public QuestDbContext(DbContextOptions<QuestDbContext> options) : base(options) { }

        public DbSet<PlayerQuestState> PlayerQuestState { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerQuestState>().HasKey(p => p.PlayerId);
            modelBuilder.Entity<PlayerQuestState>().Property(p => p.PlayerId).ValueGeneratedNever();
        }
    }
}
