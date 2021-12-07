using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.Models;
using API.Domain.Services.Communication;

namespace API.Domain.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> ListAsync();
        Task<AuthorResponse> SaveAsync(Author author);
        Task<AuthorResponse> UpdateAsync(int id, Author author);
        Task<AuthorResponse> DeleteAsync(int id);
    }
}
