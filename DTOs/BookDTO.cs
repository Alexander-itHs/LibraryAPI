namespace LibraryAPI.DTOs
{
	public class BookDTO
	{
		public int BookId { get; set; }
		public string Title { get; set; } = null!;
		public string? ISBN { get; set; } = null!;
		public DateOnly PublicationDate { get; set; }

	}
}
