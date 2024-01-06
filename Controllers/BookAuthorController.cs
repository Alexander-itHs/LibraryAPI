using LibraryAPI.DTOs;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookAuthorController : ControllerBase
{
	private readonly LibraryContext _context;

	public BookAuthorController(LibraryContext context)
	{
		_context = context;
	}
	[HttpPost]
	public async Task<ActionResult<CreateBookAuthorDTO>> PostBookAuthor(CreateBookAuthorDTO createBookAuthorDTO)
	{
		var book = await _context.Books.FindAsync(createBookAuthorDTO.BookId);

            if (book == null)
            {
                return NotFound(createBookAuthorDTO.BookId);
            }
		var author = await _context.Author.FindAsync(createBookAuthorDTO.AuthorId);

		if (author == null) 
		{
			return NotFound(createBookAuthorDTO.AuthorId);
		}
		book.Authors.Add(author);
		
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(BooksController.GetBook), new { id = book.BookId }, book);
	}
}
