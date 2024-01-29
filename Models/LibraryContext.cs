using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Models;

public class LibraryContext : DbContext
{
	
	public LibraryContext(DbContextOptions<LibraryContext> options)
		: base(options)
	{
	}
	public DbSet<Book> Book { get; set; } = null!;
	public DbSet<Author> Author { get; set; } = default!;
	public DbSet<Borrower> Borrower { get; set; } = default!;
	public DbSet<BookLoan> BookLoan { get; set; } = default!;

}
