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

        public ValueTask<IEnumerable<Book>> ListAsync()
        {
            return _bookRepository.ListAsync();
        }

		
		public async Task<IEnumerable<Book>> GetAllByAuthorAsync(int id)
		{
			var existingBook = await _bookRepository.FindByIdAsync(id);
			if (existingBook == null)
				return null;    //возврат нуля невозможен для ValueTask            

			return await _bookRepository.FindByAuthorIdAsync(existingBook.AuthorId); 
		}

		//невозможно преобразовать List в ValueTask, ошибка внутри FindByNameAsync (файл BookRepository)
		public async Task<IEnumerable<Book>> GetAllByNameAsync(string name)
		{
			return await _bookRepository.FindByNameAsync(name);	             
		}

		public async Task<ConcreteResponse<Book>> SaveAsync(Book book)
		{
			var existingBook = await _bookRepository.FindByIdAsync(book.AuthorId);
			if (existingBook == null)
				return new ConcreteResponse<Book>(TextResponses.NotFoundResponse); //здесь и далее при переходе на ValueTask возникает CS0029
			try
			{
				await _bookRepository.AddAsync(book);
				await _unitOfWork.CompleteAsync();

				return new ConcreteResponse<Book>(book);
			}
			catch (Exception ex)
			{
				return new ConcreteResponse<Book>($"{TextResponses.BadResponse} {ex.Message}");
			}
		}


		public async Task<ConcreteResponse<Book>> UpdateAsync(int id, Book book)
		{
			var existingBook = await _bookRepository.FindByIdAsync(id);

			if (existingBook == null)
				return new ConcreteResponse<Book>(TextResponses.NotFoundResponse);

			existingBook.Name = book.Name;
			existingBook.Price = book.Price;
			existingBook.Author = book.Author;
			existingBook.AuthorId = book.AuthorId;

			try
			{
				_bookRepository.Update(existingBook);
				await _unitOfWork.CompleteAsync();

				return new ConcreteResponse<Book>(existingBook);
			}
			catch (Exception ex)
			{
				return new ConcreteResponse<Book>($"{TextResponses.BadResponse} {ex.Message}");
			}
		}
		public async Task<ConcreteResponse<Book>> DeleteAsync(int id)
		{
			var existingBook = await _bookRepository.FindByIdAsync(id);

			if (existingBook == null)
				return new ConcreteResponse<Book>(TextResponses.NotFoundResponse);

			try
			{
				_bookRepository.Remove(existingBook);
				await _unitOfWork.CompleteAsync();

				return new ConcreteResponse<Book>(existingBook);
			}
			catch (Exception ex)
			{
				return new ConcreteResponse<Book>($"{TextResponses.BadResponse} {ex.Message}");
			}
		}
	}
}
