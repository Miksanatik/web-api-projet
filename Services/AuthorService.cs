using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.Models;
using API.Domain.Repositories;
using API.Domain.Services;
using API.Domain.Services.Communication;

namespace API.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        private readonly IUnitOfWork _unitOfWork;

        public AuthorService(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _unitOfWork = unitOfWork;
        }
        public  ValueTask<IEnumerable<Author>> ListAsync()
        {
            return   _authorRepository.ListAsync();
        }
        public  async Task<ConcreteResponse<Author>> SaveAsync(Author author)
        {
            try
            {
                await _authorRepository.AddAsync(author);
                await _unitOfWork.CompleteAsync();

                return new ConcreteResponse<Author>(author); //здесь и далее при переходе на ValueTask возникает CS0029
            }
            catch (Exception ex)
            {
                return new ConcreteResponse<Author>($"{TextResponses.BadResponse} {ex.Message}");
            }
        }
        public async Task<ConcreteResponse<Author>> UpdateAsync(int id, Author author)
        {
            var existingCategory = await _authorRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new ConcreteResponse<Author>(TextResponses.NotFoundResponse);

            existingCategory.Name = author.Name;

            try
            {
                _authorRepository.Update(existingCategory);
                await _unitOfWork.CompleteAsync();

                return new ConcreteResponse<Author>(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ConcreteResponse<Author>($"{TextResponses.BadResponse} {ex.Message}");
            }
        }
        public async Task<ConcreteResponse<Author>> DeleteAsync(int id)
        {
            var existingAuthor = await _authorRepository.FindByIdAsync(id);

            if (existingAuthor == null)
                return new ConcreteResponse<Author>(TextResponses.NotFoundResponse);

            try
            {
                _authorRepository.Remove(existingAuthor);
                await _unitOfWork.CompleteAsync();

                return new ConcreteResponse<Author>(existingAuthor);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ConcreteResponse<Author>($"{TextResponses.BadResponse} {ex.Message}");
            }
        }
    }
}
