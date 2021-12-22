using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Models;

namespace API.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> ListAsync();
        Task AddAsync(Book book);
        Task<Book> FindByIdAsync(int id);
        Task<IEnumerable<Book>> FindByAuthorIdAsync(int id);
        Task<IEnumerable<Book>> FindByNameAsync(string name);
        void Update(Book book);
        void Remove(Book book);
    }
}
