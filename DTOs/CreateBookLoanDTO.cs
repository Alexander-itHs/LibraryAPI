using LibraryAPI.Models;

namespace LibraryAPI.DTOs;

public class CreateBookLoanDTO
{
	public int BorrowerId { get; set; }
	public int BookId { get; set; }
	
}
