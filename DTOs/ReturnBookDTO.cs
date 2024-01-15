using LibraryAPI.Models;

namespace LibraryAPI.DTOs
{
	public class ReturnBookDTO
	{
		public int BookId { get; set; }
		public int BorrowerId { get; set; }
		public int? BorrowerRating { get; set; }
	}
}
