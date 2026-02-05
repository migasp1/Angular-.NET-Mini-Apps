namespace Domain.Entities;

public class UserBook
{
    public int UserBookId { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public bool IsFavourite { get; set; }
    public bool IsRead { get; set; }
    public Book? Book { get; set; }
}
