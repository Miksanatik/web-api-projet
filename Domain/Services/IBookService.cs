using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Models;
using API.Domain.Services.Communication;

namespace API.Domain.Services
{
    public interface IBookService
    {
        ValueTask<IEnumerable<Book>> ListAsync();
        Task<IEnumerable<Book>> GetAllByAuthorAsync(int id);
        Task<IEnumerable<Book>> GetAllByNameAsync(string name);
        Task<ConcreteResponse<Book>> SaveAsync(Book book);
        Task<ConcreteResponse<Book>> UpdateAsync(int id, Book book);
        Task<ConcreteResponse<Book>> DeleteAsync(int id);
    }
}
