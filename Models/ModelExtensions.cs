using LibraryAPI.DTOs;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LibraryAPI.Models;

public static class ModelExtensions
{
	public static Book ToBook(this CreateBookDTO createBookDTO)
	{
		return new Book
		{
			Title = createBookDTO.Title,
			ISBN = createBookDTO.ISBN,
			PublicationDate = createBookDTO.PublicationDate,
			Copies = createBookDTO.Copies
		};
	}
	public static Author ToAuthor(this CreateAuthorDTO createAuthorDTO)
	{
		return new Author
		{
			FirstName = createAuthorDTO.FirstName,
			LastName = createAuthorDTO.LastName
		};
	}
	public static Borrower ToBorrower(this  CreateBorrowerDTO createBorrowerDTO)
	{
		return new Borrower
		{
			FirstName = createBorrowerDTO.FirstName,
			LastName = createBorrowerDTO.LastName,
			StreetAddress = createBorrowerDTO.StreetAddress,
			City = createBorrowerDTO.City,
			Country = createBorrowerDTO.Country,
			PhoneNumber = createBorrowerDTO.PhoneNumber,
			Email = createBorrowerDTO.Email
		};
	}
	public static List<AuthorDTO> ToAuthorDTOs(this IEnumerable<Author> authors)
	{
		var authorDTOs = new List<AuthorDTO>();

		foreach (var author in authors)
		{
			var authorDTO = new AuthorDTO()
			{
				AuthorId = author.AuthorId,
				FirstName = author.FirstName,
				LastName = author.LastName
			};
			authorDTOs.Add(authorDTO);
		}
		return authorDTOs;
	}
	public static List<BookDTO> ToBookDTOs(this IEnumerable<Book> books)
	{
		var bookDTOs = new List<BookDTO>();

		foreach (var book in books)
		{
			var bookDTO = new BookDTO()
			{
				BookId = book.BookId,
				Title = book.Title,
				ISBN = book.ISBN,
				PublicationDate = book.PublicationDate
			};
			bookDTOs.Add(bookDTO);
		}
		return bookDTOs;
	}
	public static List<BorrowedBookDTO> ToBorrowBookDTOs(this IEnumerable<BorrowedBook> borrowedBooks)
	{
		var borrowedBookDTOs = new List<BorrowedBookDTO>();

		foreach (var borrowedBook in borrowedBooks)
		{
			var borrowedBookDTO = new BorrowedBookDTO()
			{
				BorrowedBookId = borrowedBook.BorrowedBookId,
				BorrowingDate = borrowedBook.BorrowingDate,
				ReturnDate = borrowedBook.ReturnDate,
				BorrowerRating = borrowedBook.BorrowerRating
			};
			borrowedBookDTOs.Add(borrowedBookDTO);
		}
		return borrowedBookDTOs;
	}


}
