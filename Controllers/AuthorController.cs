using BookStoreApi.Contracts;
using BookStoreApi.Data;
using BookStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/authors")]
[ApiController]
public class AuthorController(BookStoreContext context) : ControllerBase
{
    private readonly BookStoreContext _context = context;

    private bool AuthorExists(int id)
    {
        return _context.Authors.Any(e => e.Id == id);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorContract>>> GetAuthors()
    {
        var authors = await _context.Authors.Include(author => author.Books).Select(author => new AuthorContract
        {
            Id = author.Id,
            Name = author.Name,
            Books = author.Books.Select(book => new BookContract
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Category = book.Category,
                Language = book.Language,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl,
                BookPdfUrl = book.BookPdfUrl
            }).ToList()
        }).ToListAsync();

        return authors;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorContract>> GetAuthor(int id)
    {
        var author = await _context.Authors.Include(author => author.Books).Select(author => new AuthorContract{
            Id = author.Id,
            Name = author.Name,
            Books = author.Books.Select(book => new BookContract
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Category = book.Category,
                Language = book.Language,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl,
                BookPdfUrl = book.BookPdfUrl
            }).ToList()
        }).FirstOrDefaultAsync(author => author.Id == id);

        if (author == null)
        {
            return NotFound();
        }

        return author;
    }
}
