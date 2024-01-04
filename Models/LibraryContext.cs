using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;

namespace LibraryAPI.Models;

public class LibraryContext : DbContext
{
	
	public LibraryContext(DbContextOptions<LibraryContext> options)
		: base(options)
	{
	}
	public DbSet<Book> Books { get; set; } = null!;
	public DbSet<Author> Author { get; set; } = default!;

	public DbSet<Borrower> Borrower { get; set; } = default!;

public DbSet<LibraryAPI.Models.BorrowedBook> BorrowedBook { get; set; } = default!;

}
