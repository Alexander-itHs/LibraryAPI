using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;
using LibraryAPI.DTOs;

namespace LibraryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookLoansController : ControllerBase
{
    private readonly LibraryContext _context;

    public BookLoansController(LibraryContext context)
    {
        _context = context;
    }


    //GET: api/BookLoans
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookLoanDTO>>> GetBookLoans()
    {
        var bookLoans = await _context.BookLoan
            .Include(b => b.Book)
            .Include(b => b.Borrower)
            .ToListAsync();


        List<BookLoanDTO> bookLoansToReturn = new List<BookLoanDTO>();

        foreach (var bookLoan in bookLoans)
        {
            BookLoanDTO bookLoanDTO = new BookLoanDTO()
            {
                BookLoanId = bookLoan.BookLoanId,
                BookId = bookLoan.Book.BookId,
                BorrowerId = bookLoan.Borrower.BorrowerId,
                BookTitle = bookLoan.Book.Title,
                BorrowerFullName = bookLoan.Borrower.FirstName
                    + " " + bookLoan.Borrower.LastName,
                BorrowingDate = bookLoan.BorrowingDate,
                ReturnDate = bookLoan.ReturnDate,
                BorrowerRating = bookLoan.BorrowerRating
            };
            bookLoansToReturn.Add(bookLoanDTO);
        }
        return bookLoansToReturn;
    }
	// GET: api/BookLoans/5
	[HttpGet("{id}")]
	public async Task<ActionResult<GetBookLoanDTO>> GetBookLoan(int id)
    {
        var bookLoan = await _context.BookLoan
            .Include(bookLoan => bookLoan.Book)
            .ThenInclude(book => book.Authors)
            .Include(bookLoan => bookLoan.Borrower)
            .FirstOrDefaultAsync(bookLoan => bookLoan.BookLoanId == id);
        if (bookLoan == null)
        {
            return NotFound();
        }

		GetBorrowerDTO borrowerDTOToReturn = new()
		{
			BorrowerId = bookLoan.Borrower.BorrowerId,
			FirstName = bookLoan.Borrower.FirstName,
			LastName = bookLoan.Borrower.LastName,
			StreetAddress = bookLoan.Borrower.StreetAddress,
			City = bookLoan.Borrower.City,
			Country = bookLoan.Borrower.Country,
			PhoneNumber = bookLoan.Borrower.PhoneNumber,
			Email = bookLoan.Borrower.Email,
			BookLoans = bookLoan.Borrower.BookLoans.ToBookLoanDTOs()
		};

		GetBookDTO bookDTOToReturn = new()
		{
			BookId = bookLoan.Book.BookId,
			Title = bookLoan.Book.Title,
			ISBN = bookLoan.Book.ISBN,
			PublicationDate = bookLoan.Book.PublicationDate,
			Authors = bookLoan.Book.Authors.ToAuthorDTOs(),
			Copies = bookLoan.Book.Copies
		};

        

        GetBookLoanDTO bookLoanDTOToReturn = new()
        {
            BookLoanId = bookLoan.BookLoanId,
            GetBorrowerDTO = borrowerDTOToReturn,
            GetBookDTO = bookDTOToReturn,
            BorrowingDate = bookLoan.BorrowingDate,
            ReturnDate = bookLoan.ReturnDate,
            BorrowerRating = bookLoan.BorrowerRating
        };

		return bookLoanDTOToReturn;         
    }



	// POST: api/BookLoans
	// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
	[HttpPost("borrowbook")]
    public async Task<ActionResult<BookLoan>> PostBookLoan(CreateBookLoanDTO createBookLoanDTO)
    {
        Book? book = await _context.Book
            .Include(book => book.Authors)
            .FirstOrDefaultAsync(book => book.BookId == createBookLoanDTO.BookId);
        
        if (book == null)
        {
            return NotFound(createBookLoanDTO.BookId);
        }

        int currentlyBorrowedQuantity = _context.BookLoan
        .Where(b => b.Book.BookId == book.BookId && b.ReturnDate == null)
        .Count();

        int bookCopiesTotal = book.Copies;
        int booksQuantityAvailable = bookCopiesTotal - currentlyBorrowedQuantity;
        
        Borrower? borrower = await _context.Borrower
            .FirstOrDefaultAsync(book => book.BorrowerId == createBookLoanDTO.BorrowerId);

        if (borrower == null)
        {
            return NotFound(createBookLoanDTO.BorrowerId);
        }
        if (booksQuantityAvailable < 1)
        {
            return NotFound("There is currently no available copy to borrow.");
        }
        var bookLoan = new BookLoan
        {
            Book = book,
            Borrower = borrower,
            BorrowingDate = DateTime.Now
        };

        _context.BookLoan.Add(bookLoan);
        await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(GetBookLoan), new { Id = bookLoan.BookLoanId }, bookLoan);
	}
    // Patch: api/BookLoans
    [HttpPatch("returnbook")]
    public async Task<IActionResult> ReturnBook(ReturnBookDTO returnBookDTO)
    {
        BookLoan? bookLoan = _context.BookLoan
            .Include(b => b.Book)
            .Include(b => b.Borrower)
            .FirstOrDefault(bookLoan
            => bookLoan.Book.BookId == returnBookDTO.BookId
            &&
            bookLoan.Borrower.BorrowerId == returnBookDTO.BorrowerId
            &&
            bookLoan.ReturnDate == null);

        if (bookLoan == null)
        {
            return NotFound("Invalid entry.");
        }

        bookLoan.ReturnDate = DateTime.Now;
        bookLoan.BorrowerRating = returnBookDTO.BorrowerRating;


        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBookLoan), new { Id = bookLoan.BookLoanId }, bookLoan);
    }

    private bool BorrowedBookExists(int id)
    {
        return _context.BookLoan.Any(e => e.BookLoanId == id);
    }
}
