using System.Collections;
using BookStoreApi.Contracts;
using BookStoreApi.Data;
using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/books")]
[ApiController]
public class BookController(BookStoreContext context) : ControllerBase
{
    private readonly BookStoreContext _context = context;

    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookContract>>> GetBooks()
    {
        var books = await _context.Books.Include(book => book.Author).Select(book => new BookContract
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author.Name,
            Description = book.Description,
            Category = book.Category,
            Language = book.Language,
            TotalPages = book.TotalPages,
            CoverImageUrl = book.CoverImageUrl,
            BookPdfUrl = book.BookPdfUrl
        }).ToListAsync();

        return books;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookContract>> GetBook(int id)
    {
        // include related data (Author) in the response
        var book = await _context.Books.Include(book => book.Author).Select(book => new BookContract{
            Id = book.Id,
            Title = book.Title,
            Author = book.Author.Name,
            Description = book.Description,
            Category = book.Category,
            Language = book.Language,
            TotalPages = book.TotalPages,
            CoverImageUrl = book.CoverImageUrl,
            BookPdfUrl = book.BookPdfUrl
        }).FirstOrDefaultAsync(book => book.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        return book;
    }

    
    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBook", new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Book>> UpdateBook(int id, Book book)
    {
                if (id != book.Id)
        {
            return BadRequest();
        }

        _context.Books.Update(book);

        try {
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException) {
            if (!BookExists(id)) {
                return NotFound();
            } else {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}