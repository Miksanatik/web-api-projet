using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Models;

namespace API.Domain.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> ListAsync();
        Task AddAsync(Author author);
        Task<Author> FindByIdAsync(int id);
        void Update(Author author);
        void Remove(Author author);
    }
}
