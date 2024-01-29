namespace LibraryAPI.DTOs
{
	public class GetBookLoanDTO
	{
		public int BookLoanId { get; set; }
		public required GetBorrowerDTO GetBorrowerDTO { get; set; }
		public required GetBookDTO GetBookDTO { get; set; }
		public DateTime BorrowingDate { get; set; }
		public DateTime? ReturnDate { get; set; }
		public int? BorrowerRating { get; set; }
	}
}
