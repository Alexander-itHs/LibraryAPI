namespace LibraryAPI.Models;

public class Book
{
	public int BookId { get; set; }
	public string Title { get; set; } = null!;
	public string? ISBN { get; set; } = null!;
	public DateOnly PublicationDate { get; set; }
	public List<Author> Authors { get; } = new();
}
