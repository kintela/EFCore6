using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging;
using PublisherDomain;

namespace PublisherData;
public class PubContext:DbContext
{
  public DbSet<Author> Authors { get; set; }
  public DbSet<Book> Books { get; set; }
  public DbSet<Artist> Artists { get; set; }
  public DbSet<Cover> Covers { get; set; }

  public PubContext()
  {
  }

  public PubContext(DbContextOptions<PubContext> options)
    :base(options)
  {
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
    {
			//optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=PubDatabase")
			optionsBuilder.UseSqlite("Data Source=PubDatabase.db")
				.LogTo(Console.WriteLine,
					new[] { DbLoggerCategory.Database.Command.Name},
					LogLevel.Information)
				.EnableSensitiveDataLogging();

		}
	}

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Author>().HasData(
      new Author { AuthorId = 1, FirstName = "Rhoda", LastName = "Lerman" });

    var authorList = new Author[] {
    new Author { AuthorId=2, FirstName = "Ruth", LastName = "Ozeki" },
    new Author { AuthorId=3, FirstName = "Sofia", LastName = "Segovia" },
    new Author { AuthorId=4, FirstName = "Ursula K.", LastName = "LeGuin" },
    new Author { AuthorId=5, FirstName = "Hugh", LastName = "Howey" },
    new Author { AuthorId=6, FirstName = "Isabelle", LastName = "Allende" }
    };
    modelBuilder.Entity<Author>().HasData(authorList);

    var someBooks = new Book[] {
      new Book{ BookId=1, AuthorId=1, Title="In God's Ear", PublishDate=new DateTime(1989, 3,1)},
			new Book{ BookId=2, AuthorId=2, Title="A Tale For the Time Being", PublishDate=new DateTime(2013, 12,31)},
			new Book{ BookId=3, AuthorId=3, Title="The left hand of Darkness", PublishDate=new DateTime(1969, 3,1)},
      new Book{ BookId=4, AuthorId=1, Title="TAlabaster", PublishDate=new DateTime(2000, 3,1)},
      new Book{ BookId=5, AuthorId=3, Title="Wool", PublishDate=new DateTime(2010, 3,1)},
      new Book{ BookId=6, AuthorId=2, Title="Shift", PublishDate=new DateTime(2015, 3,1)},
    };

    modelBuilder.Entity<Book>().HasData(someBooks);

    var someArtists = new Artist[] {
      new Artist{ArtistId=1, FirstName="Pablo", LastName="Picasso"},
      new Artist{ArtistId=2, FirstName="Dee", LastName="Bell"},
      new Artist{ArtistId=3, FirstName="Katharine", LastName="Kuharic"}
    };
    modelBuilder.Entity<Artist>().HasData(someArtists);

    var someCovers = new Cover[] {
      new Cover{ CoverId=1,BookId=3, DesignIdeas="How about a left hand in the dark?", DigitalOnly=false},
      new Cover{ CoverId=2,BookId=2, DesignIdeas="Should we put a clock?", DigitalOnly=true},
      new Cover{ CoverId=3,BookId=1, DesignIdeas="A big ear in the clocks?", DigitalOnly=false}
    };

    modelBuilder.Entity<Cover>().HasData(someCovers);


    //example of mapping skip navigation with payload
    modelBuilder.Entity<Artist>()
        .HasMany(a => a.Covers)
        .WithMany(c => c.Artists)
        .UsingEntity<CoverAssignment>(
           join => join
            .HasOne<Cover>()
            .WithMany()
            .HasForeignKey(ca => ca.CoverId),
           join => join
            .HasOne<Artist>()
            .WithMany()
            .HasForeignKey(ca => ca.ArtistId));
    //modelBuilder.Entity<CoverAssignment>()
                //.Property(ca => ca.DateCreated).HasDefaultValueSql();
    modelBuilder.Entity<CoverAssignment>()
                 .Property(ca => ca.CoverId).HasColumnName("CoversCoverId");
    modelBuilder.Entity<CoverAssignment>()
                 .Property(ca => ca.ArtistId).HasColumnName("ArtistsArtistId");



  }
}

