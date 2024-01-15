﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<IEnumerable<Borrower>>> GetBorrower()
        {
            return await _context.Borrower.ToListAsync();
        }

        // GET: api/Borrowers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Borrower>> GetBorrower(int id)
        {
            var borrower = await _context.Borrower.FindAsync(id);

            if (borrower == null)
            {
                return NotFound();
            }

            return borrower;
        }

        // PUT: api/Borrowers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //Change to CreateBorrowerDTO
        //public async Task<IActionResult> PutBorrower(int id, CreateBorrowerDTO createBorrowerDTO)
        //{
        //    var borrower = createBorrowerDTO.ToBorrower();
        //    if (id != borrower.BorrowerId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(borrower).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BorrowerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
        public async Task<IActionResult> PutBorrower(int id, Borrower borrower)
        {
            if (id != borrower.BorrowerId)
            {
                return BadRequest();
            }

            _context.Entry(borrower).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowerExists(id))
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

        // POST: api/Borrowers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Borrower>> PostBorrower(CreateBorrowerDTO createBorrowerDTO)
        {
            var borrower = createBorrowerDTO.ToBorrower();
            _context.Borrower.Add(borrower);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBorrower", new { id = borrower.BorrowerId }, borrower);
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
