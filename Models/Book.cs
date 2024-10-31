using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models;

public class Book
{
    public int Id { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public required string Title { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public required string Author { get; set; }
    [Required]
    [StringLength(500, MinimumLength = 20)]
    public required string Description { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public required string Category { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public required string Language { get; set; }
    [Required]
    [Range(100, 5000)]
    public required int TotalPages { get; set; }
    [Required]
    [StringLength(500, MinimumLength = 5)]
    public string CoverImageUrl { get; set; } = "not available";
    [Required]
    [StringLength(500, MinimumLength = 5)]
    public string BookPdfUrl { get; set; } = "not available";
}
