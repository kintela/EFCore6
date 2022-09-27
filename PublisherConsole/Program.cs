// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

PubContext _context = new PubContext();

FindIt();


void FindIt()
{
  var authorIdTwo = _context.Authors.Find(2);
}

void QueryFilters()
{
  var name = "Josie";
  //var authors = _context.Authors.Where(s => s.FirstName == name).ToList();
  var filter = "L%";
  var authors = _context.Authors
    .Where(a => EF.Functions.Like(a.LastName,filter)).ToList();
}

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
  author.Books.Add(new Book { Title = "Programming Entity Framewok", PublishDate = new DateTime(2009, 1, 1) });
  author.Books.Add(new Book { Title = "Programming Entity Framewok 2nd Ed", PublishDate = new DateTime(2010, 8, 1) });

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

  Console.ReadLine();

}


