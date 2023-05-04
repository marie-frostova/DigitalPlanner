using DigitalPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalPlanner.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<FolderModel> Folders { get; set; }
    }
}
