using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Domain.Models;
using API.Domain.Services;
using API.Resources;
using AutoMapper;
using API.Extensions;

namespace API.Controllers
{
    [Route("/api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorsController (IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<AuthorViewModel>> GetAllAsync()
        {
            var authors = await _authorService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorViewModel>>(authors);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostNewAuthorAsync([FromBody] SaveAuthorViewModel resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var author = _mapper.Map<SaveAuthorViewModel, Author>(resource);

            var result = await _authorService.SaveAsync(author);

            if (!result.Success)
                return BadRequest(result.Message);

            var authorResource = _mapper.Map<Author, AuthorViewModel>(result.Entity);
            return Ok(authorResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveAuthorViewModel resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var author = _mapper.Map<SaveAuthorViewModel, Author>(resource);
            var result = await _authorService.UpdateAsync(id, author);

            if (!result.Success)
                return BadRequest(result.Message);

            var authorResource = _mapper.Map<Author, AuthorViewModel>(result.Entity);
            return Ok(authorResource);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _authorService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var authorResource = _mapper.Map<Author, AuthorViewModel>(result.Entity);
            return Ok(authorResource);
        }
    }
}
