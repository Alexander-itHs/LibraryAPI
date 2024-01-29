using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;
using LibraryAPI.DTOs;


namespace LibraryAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly LibraryContext _context;

		public BooksController(LibraryContext context)
		{
			_context = context;
		}

		// GET: api/Books
		[HttpGet]
		public async Task<ActionResult<IEnumerable<GetBookDTO>>> GetBooks()
		{
			var books = await _context.Book.Include(b => b.Authors).ToListAsync();

			List<GetBookDTO> bookDTOsToReturn = new List<GetBookDTO>();

			foreach (var book in books)
			{
				GetBookDTO getBookDTO = new GetBookDTO()
				{
					BookId = book.BookId,
					Title = book.Title,
					ISBN = book.ISBN,
					PublicationDate = book.PublicationDate,
					Authors = book.Authors.ToAuthorDTOs(),
					Copies = book.Copies
				};
				bookDTOsToReturn.Add(getBookDTO);
			}	

			return bookDTOsToReturn;
		}
		

		// GET: api/Books/5
		[HttpGet("{id}")]
		public async Task<ActionResult<GetBookDTO>> GetBook(int id)
		{
			var book = await _context.Book
				.Include(b => b.Authors).SingleOrDefaultAsync(b => b.BookId == id);

			if (book == null)
			{
				return NotFound();
			}
			
			GetBookDTO bookDTOToReturn = new()
			{
				BookId = book.BookId,
				Title = book.Title,
				ISBN = book.ISBN,
				PublicationDate = book.PublicationDate,
				Authors = book.Authors.ToAuthorDTOs(),
				Copies = book.Copies
			};		

			return bookDTOToReturn;
		}
		// GET: api/Books/searchTerm
		[HttpGet("search/{searchTerm}")]
		public async Task<ActionResult<IEnumerable<GetBookDTO>>> FindBooksBySearchTerm(string searchTerm)
		{
			var books = await _context.Book
				.Where(b => b.Title.Contains(searchTerm))
				.Include(b => b.Authors)
				.ToListAsync();

			List<GetBookDTO> bookDTOsToReturn = new List<GetBookDTO>();

			foreach (var book in books)
			{
				GetBookDTO getBookDTO = new GetBookDTO()
				{
					BookId = book.BookId,
					Title = book.Title,
					ISBN = book.ISBN,
					PublicationDate = book.PublicationDate,
					Authors = book.Authors.ToAuthorDTOs(),
					Copies = book.Copies

				};
				bookDTOsToReturn.Add(getBookDTO);
			}

			return bookDTOsToReturn;
		}

		// POST: api/Books
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Book>> PostBook(CreateBookDTO createBookDTO)
		{
			var book = createBookDTO.ToBook();
			_context.Book.Add(book);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
		}

		// DELETE: api/Books/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook(int id)
		{
			var book = await _context.Book.FindAsync(id);
			if (book == null)
			{
				return NotFound();
			}

			_context.Book.Remove(book);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool BookExists(int id)
		{
			return _context.Book.Any(e => e.BookId == id);
		}
	}
}
