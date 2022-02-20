using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.Models;
using API.Domain.Services.Communication;

namespace API.Domain.Services
{
    public interface IAuthorService
    {
        ValueTask<IEnumerable<Author>> ListAsync();
        Task<ConcreteResponse<Author>> SaveAsync(Author author);
        Task<ConcreteResponse<Author>> UpdateAsync(int id, Author author);
        Task<ConcreteResponse<Author>> DeleteAsync(int id);
    }
}
