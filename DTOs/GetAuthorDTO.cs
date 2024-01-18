namespace LibraryAPI.DTOs
{
	public class GetAuthorDTO
	{
		public int AuthorId { get; set; }
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public List<BookDTO> Books { get; set; } = new();
	}
}
