using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DTOs
{
	public class BookLoanDTO
	{	
		public int BookLoanId { get; set; }
		public int BookId { get; set; }
		public int BorrowerId { get; set; }
		public string BookTitle { get; set; } = null!;
		public string BorrowerFullName { get; set; } = null!;
		public DateTime BorrowingDate { get; set; }
		public DateTime? ReturnDate { get; set; }
		public int? BorrowerRating { get; set; }

	}
}
