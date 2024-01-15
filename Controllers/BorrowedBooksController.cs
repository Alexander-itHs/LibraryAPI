using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;
using LibraryAPI.DTOs;

namespace LibraryAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class BorrowedBooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BorrowedBooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/BorrowedBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BorrowedBook>>> GetBorrowedBook()
        {
            return await _context.BorrowedBook.Include(b => b.Book).Include(b => b.Borrower).ToListAsync();
        }

        // GET: api/BorrowedBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BorrowedBook>> GetBorrowedBook(int id)
        {
            var borrowedBook = await _context.BorrowedBook.FindAsync(id);

            if (borrowedBook == null)
            {
                return NotFound();
            }

            return borrowedBook;
        }

        // PUT: api/BorrowedBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBorrowedBook(int id, BorrowedBook borrowedBook)
        {
            if (id != borrowedBook.BorrowedBookId)
            {
                return BadRequest();
            }

            _context.Entry(borrowedBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowedBookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BorrowedBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("borrowbook")]
        public async Task<ActionResult<BorrowedBook>> BorrowBook(BorrowBookDTO borrowBookDTO)
        {
            Book? book = await _context.Books.FindAsync(borrowBookDTO.BookId);
			
            if (book == null)
			{
				return NotFound(borrowBookDTO.BookId);
			}

			int currentlyBorrowedQuantity = _context.BorrowedBook.Where(b => 
                b.Book.BookId == book.BookId && b.ReturnDate == null).Count();
            
            int bookCopiesTotal = book.Copies;
            int booksQuantityAvailable = bookCopiesTotal - currentlyBorrowedQuantity;
            Borrower? borrower = await _context.Borrower.FindAsync(borrowBookDTO.BorrowerId);
            
            
			if (borrower == null)
			{
				return NotFound(borrowBookDTO.BorrowerId);
			}
            if (booksQuantityAvailable < 1)
            {
                return NotFound("There is currently no available copy to borrow.");
            }
            var borrowedBook = new BorrowedBook
            {
                Book = book,
                Borrower = borrower,
                BorrowingDate = DateTime.Now
            };
            

            _context.BorrowedBook.Add(borrowedBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBorrowedBook", new { Id = borrowedBook.BorrowedBookId }, borrowedBook);
		}
        //// Patch: api/BorrowedBooks/5
        [HttpPatch("returnbook")]
        public async Task<IActionResult> ReturnBook(ReturnBookDTO returnBookDTO)
        {
            BorrowedBook? borrowedBook = _context.BorrowedBook.FirstOrDefault(borrowedBook
                => borrowedBook.Book.BookId == returnBookDTO.BookId 
                &&
                borrowedBook.Borrower.BorrowerId == returnBookDTO.BorrowerId
                &&
                borrowedBook.ReturnDate == null);

            if (borrowedBook == null)
            {
                return NotFound("Invalid entry.");
            }
            
            borrowedBook.ReturnDate = DateTime.Now;
            borrowedBook.BorrowerRating = returnBookDTO.BorrowerRating;
            
            _context.Entry(borrowedBook).State = EntityState.Modified;
            await _context.SaveChangesAsync();
          

            return NoContent();
        }

        // DELETE: api/BorrowedBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrowedBook(int id)
        {
            var borrowedBook = await _context.BorrowedBook.FindAsync(id);
            if (borrowedBook == null)
            {
                return NotFound();
            }

            _context.BorrowedBook.Remove(borrowedBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }
		

		private bool BorrowedBookExists(int id)
        {
            return _context.BorrowedBook.Any(e => e.BorrowedBookId == id);
        }
    }
}
