using Domain.VOs;

namespace Domain.Entities;

public class Book
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }
    public string Title { get; set; } = default!;
    public Isbn? Isbn { get; set; }
    public Money? Price { get; set; }
    public Author? Author { get; set; }

}
