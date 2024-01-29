using LibraryAPI.DTOs;

namespace LibraryAPI.Models;

public class BookLoan
{
	public int BookLoanId { get; set; }
	public required Borrower Borrower { get; set; }
	public required Book Book { get; set; }
	public DateTime BorrowingDate { get; set; }
	public DateTime? ReturnDate { get; set; }
	public int? BorrowerRating { get; set; }
}
