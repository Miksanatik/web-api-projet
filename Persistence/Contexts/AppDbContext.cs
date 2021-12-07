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
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Author>().ToTable("Authors");
            builder.Entity<Author>().HasKey(p => p.Id);
            builder.Entity<Author>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Author>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Author>().HasMany(p => p.Books).WithOne(p => p.Author).HasForeignKey(p => p.AuthorId);

            builder.Entity<Author>().HasData
            (
                new Author {Id = 1, Name = "Mark Twain" }, 
                new Author {Id = 2, Name = "Arthur Conan Doyle" }
            );

            builder.Entity<Book>().ToTable("Books");
            builder.Entity<Book>().HasKey(p => p.Id);
            builder.Entity<Book>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Book>().Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Entity<Book>().Property(p => p.Price).IsRequired();

            builder.Entity<Book>().HasData
            (
                new Book
                {
                    Id = 1,
                    Name = "The Adventures of Tom Sawyer",
                    Price = 10,
                    AuthorId = 1
                },
                new Book
                {
                    Id = 2,
                    Name = "sherlock holmes",
                    Price = 12,
                    AuthorId = 2,
                }
            );
        }
    }
}