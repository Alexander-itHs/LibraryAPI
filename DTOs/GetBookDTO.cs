using LibraryAPI.Models;

namespace LibraryAPI.DTOs
{
	public class GetBookDTO
	{
		public int BookId { get; set; }
		public string Title { get; set; } = null!;
		public string? ISBN { get; set; } = null!;
		public DateOnly PublicationDate { get; set; }
		public List<AuthorDTO> Authors { get; set; } = new();
		public int Copies { get; set; }
	}
}
