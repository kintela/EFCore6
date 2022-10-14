using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

namespace PubAPI
{
	public class DataLogic
	{
		PubContext _context;

		public DataLogic(PubContext context)
		{
			_context = context;
		}

		public async Task<List<AuthorDTO>> GetAllAuthors()
		{
			var authorList = await _context.Authors.ToListAsync();

			var authorDTOList=new List<AuthorDTO>();

			foreach (var author in authorList)
			{
				authorDTOList.Add(AuthorToDTO(author));
			}

			return authorDTOList;
		}

		public async Task<AuthorDTO> GetAuthorById(int id)
		{
			var author = await _context.Authors.FindAsync(id);

			if (author==null){ return null; }
			return AuthorToDTO(author);
			
		}

		public async Task<AuthorDTO> SaveNewAuthor(AuthorDTO authorDTO)
		{
			var author = AuthorFromDTO(authorDTO);
			_context.Authors.Add(author);
			await _context.SaveChangesAsync();
			return AuthorToDTO(author);

		}

		private static AuthorDTO AuthorToDTO(Author author)
		{
			return new AuthorDTO
			{
				AuthorId = author.AuthorId,
				FirstName = author.FirstName,
				LastName = author.LastName
			};
		}

		private static Author AuthorFromDTO(AuthorDTO authorDTO)
		{
			return new Author
			{
				AuthorId = authorDTO.AuthorId,
				FirstName = authorDTO.FirstName,
				LastName = authorDTO.LastName
			};
		}
	}
}
