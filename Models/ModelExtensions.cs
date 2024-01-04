using LibraryAPI.DTOs;

namespace LibraryAPI.Models;

public static class ModelExtensions
{
	public static Author ToAuthor(this CreateAuthorDTO authorDTO)
	{
		return new Author
		{
			FirstName = authorDTO.FirstName,
			LastName = authorDTO.LastName
		};
	}
}
