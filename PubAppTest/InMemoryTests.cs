using Microsoft.EntityFrameworkCore;
using PublisherConsole;
using PublisherData;
using PublisherDomain;
using System.Diagnostics;

namespace PubAppTest
{
	[TestClass]
	public class InMemoryTests
	{
		[TestMethod]
		public void CanInsertAuthorIntoDatabase()
		{
			var builder = new DbContextOptionsBuilder<PubContext>();

			builder.UseInMemoryDatabase("CanInsertAuthorIntoDatabase");

			using (var context = new PubContext(builder.Options))
			{
				var author = new Author { FirstName = "a", LastName = "b" };
				context.Authors.Add(author);

				Assert.AreEqual(EntityState.Added, context.Entry(author).State);

			}
		}

		[TestMethod]
		public void InsertAuthorsReturnsCorrectResultNumber()
		{
			var builder = new DbContextOptionsBuilder<PubContext>();

			builder.UseInMemoryDatabase("InsertAuthorsReturnsCorrectResultNumber");

			var authorList = new List<ImportAuthorDTO>()
			{
				new ImportAuthorDTO("a", "b"),
				new ImportAuthorDTO("c","d"),
				new ImportAuthorDTO("e", "f")
			};

			var dl = new DataLogic(new PubContext(builder.Options));
			var result = dl.ImportAuthors(authorList);

			Assert.AreEqual(authorList.Count, result);

		}
	}
}