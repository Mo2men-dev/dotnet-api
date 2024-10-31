using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models;

public class Author
{
    public int Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public required string Name { get; set; }
    public virtual ICollection<Book> Books { get; set; } = [];
}
