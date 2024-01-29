using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;
using LibraryAPI.DTOs;

namespace LibraryAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class BorrowersController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BorrowersController(LibraryContext context)
        {
            _context = context;
        }

        // GET: api/Borrowers
        [HttpGet]
		public async Task<ActionResult<IEnumerable<GetBorrowerDTO>>> GetBorrowers()
        {
            var borrowers = await _context.Borrower
                .Include(b => b.BookLoans)
                .ThenInclude(b => b.Book)
                .ThenInclude(b => b.Authors)
                .ToListAsync();

            List<GetBorrowerDTO> borrowerDTOsToReturn = new List<GetBorrowerDTO>();

            foreach(var borrower in borrowers)
            {
                GetBorrowerDTO getBorrowerDTO = new GetBorrowerDTO()
                {
                    BorrowerId = borrower.BorrowerId,
                    FirstName = borrower.FirstName,
                    LastName = borrower.LastName,
                    StreetAddress = borrower.StreetAddress,
                    City = borrower.City,
                    Country = borrower.Country,
                    PhoneNumber = borrower.PhoneNumber,
                    Email = borrower.Email,
                    BookLoans = borrower.BookLoans.ToBookLoanDTOs()
                };
                borrowerDTOsToReturn.Add(getBorrowerDTO);
            }
            return borrowerDTOsToReturn;
        }


		// POST: api/Borrowers
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
        public async Task<ActionResult<Borrower>> PostBorrower(CreateBorrowerDTO createBorrowerDTO)
        {
            var borrower = createBorrowerDTO.ToBorrower();
            _context.Borrower.Add(borrower);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBorrowers", new { id = borrower.BorrowerId }, borrower);
        }

        // DELETE: api/Borrowers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrower(int id)
        {
            var borrower = await _context.Borrower.FindAsync(id);
            if (borrower == null)
            {
                return NotFound();
            }

            _context.Borrower.Remove(borrower);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BorrowerExists(int id)
        {
            return _context.Borrower.Any(e => e.BorrowerId == id);
        }
    }
}
