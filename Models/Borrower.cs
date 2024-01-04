using System.Numerics;

namespace LibraryAPI.Models;

public class Borrower
{
	public int BorrowerId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string StreetAddress { get; set; }
	public string City { get; set; }
	public string Country { get; set; }
	public string PhoneNumber { get; set; }
	public string Email { get; set; }
	public List<Book>? Books { get; set; }

}
