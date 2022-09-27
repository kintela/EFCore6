// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

PubContext _context = new PubContext();

RetrieveAndUpdateMultipleAuthors();

void GetAuthors()
{
  using var context = new PubContext();
  var authors = context.Authors.ToList();
  foreach (var author in authors)
  {
    Console.WriteLine(author.FirstName + " " + author.LastName);
  }
}

void AddAuthor()
{
  var author = new Author { FirstName = "Josie", LastName = "Newf" };
  using var context = new PubContext();
  context.Authors.Add(author);
  context.SaveChanges();

}

void AddAuthorWithBook()
{
  var author = new Author { FirstName = "Julie", LastName = "Lerman" };
  author.Books.Add(new Book { Title = "Programming Entity Framework", PublishDate = new DateTime(2009, 1, 1) });
	author.Books.Add(new Book { Title = "Programming Entity Framework 2nd Ed", PublishDate = new DateTime(2010, 8, 1) });

  using var context = new PubContext();
  context.Authors.Add(author);
  context.SaveChanges();
}

void GetAuthorsWithBooks()
{
  using var context = new PubContext();

  var authors = context.Authors.Include(a => a.Books).ToList();
  foreach (var author in authors)
  {
    Console.WriteLine(author.FirstName + " " + author.LastName);
    foreach (var book in author.Books)
    {
      Console.WriteLine("*" + book.Title);
    }
  }
}

void AddSomeAuthors()
{
  _context.Authors.Add(new Author { FirstName ="Rhoda", LastName="Lerman"});
	_context.Authors.Add(new Author { FirstName = "Don", LastName = "Jones" });
	_context.Authors.Add(new Author { FirstName = "Jim", LastName = "Christopher" });
	_context.Authors.Add(new Author { FirstName = "Stephen", LastName = "Haunts" });
  _context.SaveChanges();
}

void SkipAndTakeAuthors()
{
  var groupSize = 2;
  for (int i = 0; i < 5; i++)
  {
    var authors = _context.Authors.Skip(groupSize * i).Take(groupSize).ToList();
    Console.WriteLine($"Group {i}:");
    foreach (var author in authors)
    {
      Console.WriteLine($"{author.FirstName} {author.LastName}");
    }
  }
}

void SortAuthors()
{
  var authorsByLastName = _context.Authors
    .OrderBy(a => a.LastName)
    .ThenBy(a=>a.FirstName)
    .ToList();
  authorsByLastName.ForEach(a => Console.WriteLine(a.LastName + "," + a.FirstName));
}

void QueryAggregate()
{
	var author = _context.Authors
    .OrderByDescending(a=>a.FirstName)
    .FirstOrDefault(a => a.LastName == "Lerman");
}

void InsertAuthor()
{
  var author = new Author { FirstName = "Roberto", LastName = "Quintela" };
  _context.Authors.Add(author);
  _context.SaveChanges();
}

void RetrieveAndUpdateAuthor()
{
  var author = _context.Authors.FirstOrDefault(a => a.FirstName == "Julie" && a.LastName == "Lerman");
  if (author!=null)
  {
    author.FirstName = "Julia";
    _context.SaveChanges();
  }
}

void RetrieveAndUpdateMultipleAuthors()
{
  var LermanAuthors = _context.Authors.Where(a => a.LastName == "Lerman").ToList();
  foreach (var la in LermanAuthors)
  {
    la.LastName = "Lehrman";
  }

  Console.WriteLine("Before" + _context.ChangeTracker.DebugView.ShortView);
  _context.ChangeTracker.DetectChanges();
  Console.WriteLine("After:" + _context.ChangeTracker.DebugView.ShortView);
   
  _context.SaveChanges();
}


