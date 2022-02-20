using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Models;

namespace API.Domain.Repositories
{
    public interface IBookRepository
    {
        ValueTask<IEnumerable<Book>> ListAsync();
        Task AddAsync(Book book);
        ValueTask<Book> FindByIdAsync(int id);
        Task<IEnumerable<Book>> FindByAuthorIdAsync(int id);
        Task<IEnumerable<Book>> FindByNameAsync(string name);
        void Update(Book book);
        void Remove(Book book);
    }
}
