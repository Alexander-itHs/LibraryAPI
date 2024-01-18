using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DTOs
{
	public class BorrowedBookDTO
	{
		public int BorrowedBookId { get; set; }
		public string Title { get; set; } = null!;
		public DateTime BorrowingDate { get; set; }
		public DateTime? ReturnDate { get; set; }
		public int? BorrowerRating { get; set; }

		//public async Task<ActionResult<Author>> PostAuthor(CreateAuthorDTO createAuthorDTO)
		//private async Task<ActionResult><Borrower>> GetTitle(BorredBookDTO title)
		//{
		//	title = await _context.borrewedBook.
		//}
	}
}
