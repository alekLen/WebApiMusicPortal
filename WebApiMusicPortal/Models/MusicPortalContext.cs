using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace WebApiMusicPortal.Models
{
    public class MusicPortalContext : DbContext
    {
        public MusicPortalContext(DbContextOptions<MusicPortalContext> options)
         : base(options)
        {
            if (Database.EnsureCreated())
            {
                
            }
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Salt> Salts { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}
