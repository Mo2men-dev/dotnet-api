using BookStoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Data;

public class BookStoreContext(DbContextOptions<BookStoreContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasMany(author => author.Books).WithOne(book => book.Author).HasForeignKey(book => book.AuthorId);
    }
}
