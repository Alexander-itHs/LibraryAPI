using LibraryAPI.Models;

namespace LibraryAPI.DTOs
{
	public class GetBorrowerDTO
	{
		public int BorrowerId { get; set; }
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string StreetAddress { get; set; } = null!;
		public string City { get; set; } = null!;
		public string Country { get; set; } = null!;
		public string PhoneNumber { get; set; } = null!;
		public string Email { get; set; } = null!;
		public List<BookLoanDTO> BookLoans { get; set; } = new();
	}
}
