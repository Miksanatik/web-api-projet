using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain.Models;
using API.Domain.Repositories;
using API.Domain.Services;
using API.Domain.Services.Communication;

namespace API.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
		private readonly IUnitOfWork _unitOfWork;

		public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
			_unitOfWork = unitOfWork;
		}

        public async Task<IEnumerable<Book>> ListAsync()
        {
            return await _bookRepository.ListAsync();
        }

		public async Task<BookResponse> SaveAsync(Book book)
		{
			var existingBook = await _bookRepository.FindByIdAsync(book.AuthorId);
			if (existingBook == null)
				return new BookResponse("Invalid book.");
			try
			{
				await _bookRepository.AddAsync(book);
				await _unitOfWork.CompleteAsync();

				return new BookResponse(book);
			}
			catch (Exception ex)
			{
				// Do some logging stuff
				return new BookResponse($"An error occurred when saving the book: {ex.Message}");
			}
		}

		public async Task<BookResponse> UpdateAsync(int id, Book book)
		{
			var existingBook = await _bookRepository.FindByIdAsync(id);

			if (existingBook == null)
				return new BookResponse("Book not found.");

			existingBook.Name = book.Name;
			existingBook.Price = book.Price;
			existingBook.Author = book.Author;
			existingBook.AuthorId = book.AuthorId;

			try
			{
				_bookRepository.Update(existingBook);
				await _unitOfWork.CompleteAsync();

				return new BookResponse(existingBook);
			}
			catch (Exception ex)
			{
				return new BookResponse($"An error occurred when updating the book: {ex.Message}");
			}
		}
		public async Task<BookResponse> DeleteAsync(int id)
		{
			var existingBook = await _bookRepository.FindByIdAsync(id);

			if (existingBook == null)
				return new BookResponse("Book not found.");

			try
			{
				_bookRepository.Remove(existingBook);
				await _unitOfWork.CompleteAsync();

				return new BookResponse(existingBook);
			}
			catch (Exception ex)
			{
				return new BookResponse($"An error occurred when deleting the book: {ex.Message}");
			}
		}
	}
}
