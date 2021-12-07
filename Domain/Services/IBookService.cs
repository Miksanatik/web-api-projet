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
        Task<IEnumerable<Book>> ListAsync();
        Task<BookResponse> SaveAsync(Book book);
        Task<BookResponse> UpdateAsync(int id, Book book);
        Task<BookResponse> DeleteAsync(int id);
    }
}
