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
    public abstract class BaseRepository<T> where T: class
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async ValueTask<IEnumerable<T>> ListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public  ValueTask<T> FindByIdAsync(int id)
        {
            return  _context.Set<T>().FindAsync(id);
        }
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}