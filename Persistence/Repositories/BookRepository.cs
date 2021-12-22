using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using API.Domain.Models;
using API.Domain.Repositories;
using API.Persistence.Contexts;

namespace API.Persistence.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Book>> ListAsync()
        {
            return await _context.Books.Include(p => p.Author)
                                          .ToListAsync();
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task<Book> FindByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> FindByAuthorIdAsync(int id)
        {
            return  await _context.Books.Where(p => p.AuthorId == id)
                .Include(p => p.Author)
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>> FindByNameAsync(string name)
        {
            return await _context.Books.Where(p => p.Name == name)
                .Include(p => p.Author)
                .ToListAsync();
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
        }

        public void Remove(Book book)
        {
            _context.Books.Remove(book);
        }
    }
}
