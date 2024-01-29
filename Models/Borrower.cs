namespace LibraryAPI.Models;

public class Borrower
{
	public int BorrowerId { get; set; }
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public string StreetAddress { get; set; } = null!;
	public string City { get; set; } = null!;
	public string Country { get; set; } = null!;
	public string PhoneNumber { get; set; } = null!;
	public string Email { get; set; } = null!;
	public List<BookLoan> BookLoans { get; } = new();

}
