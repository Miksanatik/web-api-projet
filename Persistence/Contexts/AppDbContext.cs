using Microsoft.EntityFrameworkCore;
using API.Domain.Models;

namespace API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {        
        }
    }
}