using LibraryAPI.DTOs;

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

}
