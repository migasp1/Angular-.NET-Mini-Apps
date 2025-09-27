namespace Domain.Entities;

public class Author
{
    public int AuthorId { get; set; }
    public string Name { get; set; } = default!;
    public DateTime Birthdate { get; set; }
    public ICollection<Book>? Books { get; set; }
}
