using System;

namespace BookStoreApi.Contracts;

public class AuthorContract
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<BookContract> Books { get; set; } = [];
}
