using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using API.Domain.Models;
using API.Domain.Services;
using API.Resources;
using API.Extensions;

namespace Supermarket.API.Controllers
{
    [Route("/api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<BookResource>> ListAsync()
        {
            var books = await _bookService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Book>, IEnumerable<BookResource>>(books);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveBookResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var book = _mapper.Map<SaveBookResource, Book>(resource);

            var result = await _bookService.SaveAsync(book);

            if (!result.Success)
                return BadRequest(result.Message);

            var bookResource = _mapper.Map<Book, BookResource>(result.Book);
            return Ok(bookResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveBookResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = _mapper.Map<SaveBookResource, Book>(resource);
            var result = await _bookService.UpdateAsync(id, category);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Book, BookResource>(result.Book);
            return Ok(categoryResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _bookService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = _mapper.Map<Book, BookResource>(result.Book);
            return Ok(categoryResource);
        }
    }
}