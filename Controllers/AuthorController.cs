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
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
    {
        return await _context.Authors.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Author>> GetAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        return author;
    }
}
