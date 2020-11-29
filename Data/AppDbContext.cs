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

        public DbSet<Result> Results{ get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<PasswordCategorie> PasswordCategories { get; set; }
    }
}
