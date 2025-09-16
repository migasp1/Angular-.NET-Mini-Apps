namespace Domain.Entities;

public class Author
{
    public int AuthorId { get; set; }
    public string Name { get; set; } = default!;
    public int BirthYear { get; set; } = default!;
    public ICollection<Book>? Books { get; set; }
}
