﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;
using LibraryAPI.DTOs;

namespace LibraryAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public AuthorsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
		public async Task<ActionResult<IEnumerable<GetAuthorDTO>>> GetAuthors()
		{
			var authors = await _context.Author.Include(a => a.Books).ToListAsync();
			
            List<GetAuthorDTO> authorDTOsToReturn = new List<GetAuthorDTO>();

            foreach (var author in authors) 
            {
                GetAuthorDTO getAuthorDTO = new GetAuthorDTO()
                {
                    AuthorId = author.AuthorId,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Books = author.Books.ToBookDTOs(),
                };
                authorDTOsToReturn.Add(getAuthorDTO);                
            }

            return authorDTOsToReturn;
		}

		// GET: api/Authors/5
		[HttpGet("{id}")]
        public async Task<ActionResult<GetAuthorDTO>> GetAuthor(int id)
        {
            var author = await _context.Author
                .Include(a => a.Books).FirstOrDefaultAsync(a => a.AuthorId == id);

            if (author == null)
            {
                return NotFound();
            }

            GetAuthorDTO authorDTOToReturn = new()
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Books = author.Books.ToBookDTOs()
            };

            return authorDTOToReturn;
        }

		// POST: api/Authors
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]  
        public async Task<ActionResult<Author>> PostAuthor(CreateAuthorDTO createAuthorDTO)
        {
            var author = createAuthorDTO.ToAuthor();
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.AuthorId }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Author.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Author.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Author.Any(e => e.AuthorId == id);
        }
    }
}
