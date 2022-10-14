using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;
using System.Diagnostics;

namespace PubAppTest
{
	[TestClass]
	public class DatabaseTests
	{
		[TestMethod]
		public void CanInsertAuthorIntoDatabase()
		{
			var builder = new DbContextOptionsBuilder<PubContext>();

			builder.UseSqlite("Data Source=PubTestDatabase.db");

			using (var context = new PubContext(builder.Options))
			{
				context.Database.EnsureDeleted();
				context.Database.EnsureCreated();
				var author = new Author { FirstName = "a", LastName = "b" };
				context.Authors.Add(author);
				Debug.WriteLine($"Before save : {author.AuthorId}");
				context.SaveChanges();
				Debug.WriteLine($"After save: {author.AuthorId}");

				Assert.AreNotEqual(0, author.AuthorId);

			}
		}
	}
}