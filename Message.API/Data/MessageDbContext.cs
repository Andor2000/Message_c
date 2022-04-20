using Message.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Message.API.Data
{
    public class MessageDbContext:DbContext
    {
        public MessageDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<Themes> Themes { get; set; }
        public DbSet<Users> Users { get; set; }

    }
}
