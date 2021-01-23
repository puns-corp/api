using Microsoft.EntityFrameworkCore;
using PunsApi.Models;

namespace PunsApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Result> Results { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Password> Passwords { get; set; }

        public DbSet<PasswordCategory> PasswordCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasOne(g => g.GameMaster)
                .WithOne(p => p.MasteredGame)
                .HasForeignKey<Game>(g => g.GameMasterId);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.MasteredGame)
                .WithOne(g => g.GameMaster)
                .HasForeignKey<Player>(x => x.MasteredGameId);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.ShowingPlayer)
                .WithOne(p => p.GameWhereCurrentlyShowing)
                .HasForeignKey<Game>(g => g.ShowingPlayerId);

            modelBuilder.Entity<Player>()
                .HasOne(p => p.GameWhereCurrentlyShowing)
                .WithOne(g => g.ShowingPlayer)
                .HasForeignKey<Player>(x => x.GameWhereCurrentlyShowingId);


        }
    }

}
