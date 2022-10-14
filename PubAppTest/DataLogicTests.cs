using Microsoft.EntityFrameworkCore;
using PublisherConsole;
using PublisherData;
using PublisherDomain;
using System.Diagnostics;
using PubAPI;
using DataLogic = PubAPI.DataLogic;

namespace PubAppTest
{
	[TestClass]
	public class DataLogicTests
	{
		[TestMethod]
		public void CanGetAnAuthorById()
		{
			var builder = new DbContextOptionsBuilder<PubContext>();

			builder.UseInMemoryDatabase("CanGetAnAuthorById");

			int seedId = SeedOneAuthor(builder.Options);

			using (var context = new PubContext(builder.Options))
			{
				var bizLogic = new DataLogic(context);
				var authorRetrieved=bizLogic.GetAuthorById(seedId);

				Assert.AreEqual(seedId, authorRetrieved.Result.AuthorId);

			}
		}

		private int SeedOneAuthor(DbContextOptions<PubContext> options)
		{
			using (var seedContext=new PubContext(options))
			{
				var author = new Author { FirstName = "a", LastName = "b" };
				seedContext.Authors.Add(author);
				seedContext.SaveChanges();
				return author.AuthorId;
			}
		}

	
	}
}