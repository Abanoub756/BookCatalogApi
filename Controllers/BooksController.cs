using BookCatalogApi.Models;
using BookCatalogApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() => Ok(BookRepository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = BookRepository.GetById(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author) || book.PageCount < 0)
                return BadRequest("Invalid book data.");

            var added = BookRepository.Add(book);
            return CreatedAtAction(nameof(GetById), new { id = added.BookId }, added);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author) || book.PageCount < 0)
                return BadRequest("Invalid book data.");

            return BookRepository.Update(id, book) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) =>
            BookRepository.Delete(id) ? NoContent() : NotFound();

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string? title, [FromQuery] string? author)
        {
            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(author))
                return NotFound("Please provide a title or author to search.");

            var results = BookRepository.Search(title, author);

            return results.Count == 0
                ? NotFound("No books matched the search criteria.")
                : Ok(results);
        }

    }

}
