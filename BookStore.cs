using Microsoft.AspNetCore.Mvc;
using SmartLibrary.API.Data;
using SmartLibrary.API.Models;

namespace SmartLibrary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    // GET: api/books
    [HttpGet]
    public ActionResult<List<Book>> GetBooks()
    {
        return Ok(BookStore.Books);
    }

    // GET: api/books/1
    [HttpGet("{id}")]
    public ActionResult<Book> GetBook(int id)
    {
        var book = BookStore.Books.FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound();
        return Ok(book);
    }

    // POST: api/books
    [HttpPost]
    public ActionResult<Book> AddBook(Book book)
    {
        // Gjeneron id automatikisht
        book.Id = BookStore.Books.Any() ? BookStore.Books.Max(b => b.Id) + 1 : 1;
        BookStore.Books.Add(book);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    // PUT: api/books/1
    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id, Book updatedBook)
    {
        var book = BookStore.Books.FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound();

        // Perditeson te dhenat e librit
        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        book.Category = updatedBook.Category;
        book.PublishedYear = updatedBook.PublishedYear;
        book.AvailableCopies = updatedBook.AvailableCopies;
        book.Rating = updatedBook.Rating;
        book.CoverUrl = updatedBook.CoverUrl;

        return NoContent();
    }

    // DELETE: api/books/1
    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var book = BookStore.Books.FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound();

        BookStore.Books.Remove(book);
        return NoContent();
    }
}
