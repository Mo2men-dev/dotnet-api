using BookStoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Data;

public class BookStoreContext(DbContextOptions<BookStoreContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();

}
