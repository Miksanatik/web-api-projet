using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Domain.Models;
using API.Domain.Repositories;
using API.Persistence.Contexts;
namespace API.Persistence.Repositories
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Author>> ListAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
        }
        public async Task<Author> FindByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public void Update(Author author)
        {
            _context.Authors.Update(author);
        }
        public void Remove(Author author)
        {
            _context.Authors.Remove(author);
        }
    }
}
