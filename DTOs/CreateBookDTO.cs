namespace LibraryAPI.DTOs;

public class CreateBookDTO
{
	public string Title { get; set; } = null!;
	public string? ISBN { get; set; } = null!;
	public DateOnly PublicationDate { get; set; }
	public int Copies { get; set; }
}
