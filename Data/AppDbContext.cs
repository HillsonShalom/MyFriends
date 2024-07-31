using Microsoft.EntityFrameworkCore;
using MyFriends.Models;

namespace MyFriends.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Friend> Friends { get; set; } = default!;
        public DbSet<Pictures> Pictures { get; set; } = default!;
    }
}
