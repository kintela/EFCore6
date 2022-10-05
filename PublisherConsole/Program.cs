// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;


using (PubContext context = new PubContext())
{
  context.Database.EnsureCreated();
}

PubContext _context = new PubContext();

GetAllBooksWithTheirCovers();

void GetAllBooksWithTheirCovers()
{
  var booksAndCovers = _context.Books.Include(b => b.Cover).ToList();
  booksAndCovers.ForEach(book => {
    Console.WriteLine(
      book.Title +
      (book.Cover==null
        ? ": No cover yet"
        : ": " + book.Cover.DesignIdeas

      ));
  });

  Console.ReadLine();
}

void ReassignCover()
{
  var coverWithArtist4 = _context.Covers
    .Include(c => c.Artists.Where(a => a.ArtistId == 4))
    .FirstOrDefault(c => c.CoverId == 6);

  coverWithArtist4.Artists.RemoveAt(0);
  var artist3 = _context.Artists.Find(3);

  coverWithArtist4.Artists.Add(artist3);

  _context.ChangeTracker.DetectChanges();
  var debugView = _context.ChangeTracker.DebugView.ShortView;
  _context.SaveChanges();
}

void UnAssignAnArtistFromACover()
{
  var coverWithArtist = _context.Covers
    .Include(c => c.Artists.Where(a => a.ArtistId == 1))
    .FirstOrDefault(c => c.CoverId == 1);

  coverWithArtist.Artists.RemoveAt(0);
  _context.ChangeTracker.DetectChanges();
  var debugView = _context.ChangeTracker.DebugView.ShortView;
  _context.SaveChanges();
}

void RetrieveAllArtistsWithTheirCovers()
{
  var artistsWithCovers = _context.Artists.Include(a => a.Covers).ToList();

  artistsWithCovers.ForEach(a =>
  {
    Console.WriteLine($"{a.FirstName} {a.LastName}, Designs to work on:");
    var primaryArtistId = a.ArtistId;
    if (a.Covers.Count()==0)
    {
      Console.WriteLine(" No Covers");
    }
    else
    {
      a.Covers.ForEach(c=>
      {
        string collaborators = "";
        c.Artists.Where(ca => ca.ArtistId != primaryArtistId).ToList()
        .ForEach(ca => collaborators += $"{ca.FirstName} {ca.LastName}");
        if (collaborators.Length>0)
        {
          collaborators = $"(with {collaborators})";
        }
        Console.WriteLine($"   *{c.DesignIdeas} {collaborators}");
      });
    }
  });

  Console.ReadLine();
}

void retrieveAllArtistsWhoHaveCovers()
{
  var artistsWithCovers = _context.Artists.Where(a => a.Covers.Any()).ToList();
}

void RetrieveAllArtistsWithTeirsCovers()
{
  var artistsWithCovers = _context.Artists.Include(a => a.Covers).ToList();
}

void RetrieveACoverWithItsArtists()
{
  var coverWithArtists = _context.Covers.Include(c => c.Artists).FirstOrDefault(c => c.CoverId == 1);
}

void RetrieveAnArtistWithTheirCovers()
{
  var artistWithCovers = _context.Artists.Include(a => a.Covers).FirstOrDefault(a => a.ArtistId == 1);
}

void CreateNewCoverAndArtistTogether()
{
  var newArtist = new Artist { FirstName = "Kir", LastName = "Talmage" };
  var newCover = new Cover { DesignIdeas = "We like birds!" };

  newArtist.Covers.Add(newCover);
  _context.Artists.Add(newArtist);
  _context.SaveChanges();
}

void CreateNewCoverWithExistingArtist()
{
  var artistA = _context.Artists.Find(1);
  var cover = new Cover { DesignIdeas = "Author has provided a photo" };

  cover.Artists.Add(artistA);
  _context.Covers.Add(cover);
  _context.SaveChanges();
}

void ConnectExistingArtistAndCoverObjects()
{
  var artistA = _context.Artists.Find(1);
  var artistB = _context.Artists.Find(2);
  var coverA = _context.Covers.Find(1);

  coverA.Artists.Add(artistA);
  coverA.Artists.Add(artistB);
  _context.SaveChanges();
}

void CascadeDeleteInActionWhenTracked()
{
  var author = _context.Authors.Include(a => a.Books)
    .FirstOrDefault(a => a.AuthorId == 7);
  _context.Authors.Remove(author);
  var state = _context.ChangeTracker.DebugView.ShortView;
  _context.SaveChanges();


}

void ModifyingRelatedDataWhenNotTracked()
{
  var author = _context.Authors.Include(a => a.Books)
    .FirstOrDefault(a => a.AuthorId == 5);

  author.Books[0].BasePrice = (decimal)12.00;

  var newContext = new PubContext();
  newContext.Entry(author.Books[0]).State = EntityState.Modified;
  var state = newContext.ChangeTracker.DebugView.ShortView;
  newContext.SaveChanges();
}

void FilterUsingRelatedData()
{
  var recentAuthors = _context.Authors
    .Where(a => a.Books.Any(b => b.PublishDate.Year >= 2015))
    .ToList();
}

void ExplicitLoadCollection()
{
  var author = _context.Authors.FirstOrDefault(a => a.LastName == "Lerman");
  _context.Entry(author).Collection(a => a.Books).Load();
}

void Projections()
{
  var unknownTypes = _context.Authors
    .Select(a => new
    {
      AuthorId = a.AuthorId,
      Name = a.FirstName.First() + "" + a.LastName,
      Books=a.Books
    })
    .ToList();

  var debugview = _context.ChangeTracker.DebugView.ShortView;
}

void EagerLoadBooksWithAuthors()
{
  var pubDateStart = new DateTime(2010, 1, 1);
  var authors = _context.Authors
    .Include(a => a.Books
                  .Where(b => b.PublishDate >= pubDateStart)
                  .OrderBy(b => b.Title))
    .ToList();

  authors.ForEach(a=>
  {
    Console.WriteLine($"{a.LastName} ({a.Books.Count})");
    a.Books.ForEach(b=>Console.WriteLine("    "+b.Title));
  });

  Console.ReadLine();
}

void AddNewBookToExstingAuthorInMemoryViaBook()
{
  var book = new Book
  {
    Title = "Shift",
    PublishDate = new DateTime(2012, 1, 1),
    AuthorId = 5
  };
  _context.Books.Add(book);
  _context.SaveChanges();
}


void AddNewBookToExstingAuthorInMemort()
{
  var author = _context.Authors.FirstOrDefault(a => a.LastName == "Howey");
  if (author!=null)
  {
    author.Books.Add(
      new Book { Title="Wool", PublishDate=new DateTime(2012,1,1)}
      );
  }

  _context.SaveChanges();
}

void InsertNewAuthorWith2NewBooks()
{
  var author = new Author { FirstName = "Don", LastName = "Jones" };
  author.Books.AddRange(new List<Book>
  {
    new Book {Title = "The Never", PublishDate = new DateTime(2019, 12, 1)},
    new Book {Title = "Alabaster", PublishDate = new DateTime(2019, 4, 1)}
  });

  _context.Authors.Add(author);
  _context.SaveChanges();
}

void InsertNewAuthorWithNewBook()
{
  var author = new Author { FirstName = "Lynda", LastName = "Rutledge" };
  author.Books.Add(new Book
  {
    Title="West With Giraffes",
    PublishDate=new DateTime (2021,2,1)
  });

  _context.Authors.Add(author);
  _context.SaveChanges();
}

void GetAuthors()
{
  var name = "Ozeki";
  var authors = _context.Authors.Where(a=>a.LastName==name).ToList();
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

void CoordinateRetrieveAndUpdateAuthor()
{
  var author = FindThatAuthor(3);
  if (author?.FirstName=="Julie")
  {
    author.FirstName = "Julia";
    SaveThatAuthor(author);
  }
}

Author FindThatAuthor(int authorId)
{
  using var shortLivedContext = new PubContext();
  return shortLivedContext.Authors.Find(authorId);
}

void SaveThatAuthor(Author author)
{
  using var anotherShortLivedContext = new PubContext();
  anotherShortLivedContext.Authors.Update(author);
  anotherShortLivedContext.SaveChanges();
}

void DeleteAnAuthor()
{
  var extraJL = _context.Authors.Find(1);
  if (extraJL!=null)
  {
    _context.Authors.Remove(extraJL);
    _context.SaveChanges();
  }
}

void InsertMultipleAuthors()
{
	var authorList = new Author[] {
    new Author { FirstName = "Ruth", LastName = "Ozeki" },
    new Author { FirstName = "Sofia", LastName = "Segovia" },
    new Author { FirstName = "Ursula K.", LastName = "LeGuin" },
    new Author { FirstName = "Hugh", LastName = "Howey" },
    new Author { FirstName = "Isabelle", LastName = "Allende" }
  };

	_context.Authors.AddRange(authorList);

	_context.SaveChanges();
}

void InsertMultipleAuthorsPassedIn(List<Author> listOfAuthors)
{ 
  _context.AddRange(listOfAuthors);
  _context.SaveChanges();
}

void BulkAddUpdate()
{
	var newAuthors = new Author[] {
		new Author { FirstName = "Tsisi", LastName = "Dangaremga" },
		new Author { FirstName = "Lisa", LastName = "See" },
		new Author { FirstName = "Zhang", LastName = "Ling" },
		new Author { FirstName = "Marilyne", LastName = "Robinson" }
	};

  _context.Authors.AddRange(newAuthors);
  var book = _context.Books.Find(2);
  book.Title = "Programming Entity Framework 2nd Edition";
  _context.SaveChanges();
}


