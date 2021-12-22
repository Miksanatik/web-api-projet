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
        public async Task<IEnumerable<Author>> ListAsync()
        {
            return await _authorRepository.ListAsync();
        }
        public async Task<AuthorResponse> SaveAsync(Author author)
        {
            try
            {
                await _authorRepository.AddAsync(author);
                await _unitOfWork.CompleteAsync();

                return new AuthorResponse(author);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new AuthorResponse($"{TextResponses.BadResponse} {ex.Message}");
            }
        }
        public async Task<AuthorResponse> UpdateAsync(int id, Author author)
        {
            var existingCategory = await _authorRepository.FindByIdAsync(id);

            if (existingCategory == null)
                return new AuthorResponse(TextResponses.NotFoundResponse);

            existingCategory.Name = author.Name;

            try
            {
                _authorRepository.Update(existingCategory);
                await _unitOfWork.CompleteAsync();

                return new AuthorResponse(existingCategory);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new AuthorResponse($"{TextResponses.BadResponse} {ex.Message}");
            }
        }
        public async Task<AuthorResponse> DeleteAsync(int id)
        {
            var existingAuthor = await _authorRepository.FindByIdAsync(id);

            if (existingAuthor == null)
                return new AuthorResponse(TextResponses.NotFoundResponse);

            try
            {
                _authorRepository.Remove(existingAuthor);
                await _unitOfWork.CompleteAsync();

                return new AuthorResponse(existingAuthor);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new AuthorResponse($"{TextResponses.BadResponse} {ex.Message}");
            }
        }
    }
}
