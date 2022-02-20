using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Models;

namespace API.Domain.Repositories
{
    public interface IAuthorRepository
    {
        ValueTask<IEnumerable<Author>> ListAsync();
        Task AddAsync(Author author);
        ValueTask<Author> FindByIdAsync(int id);
        void Update(Author author);
        void Remove(Author author);
    }
}
