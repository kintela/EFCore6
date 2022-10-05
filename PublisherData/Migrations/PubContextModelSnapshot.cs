﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PublisherData;

#nullable disable

namespace PublisherData.Migrations
{
    [DbContext(typeof(PubContext))]
    partial class PubContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("PublisherDomain.Artist", b =>
                {
                    b.Property<int>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ArtistId");

                    b.ToTable("Artists");

                    b.HasData(
                        new
                        {
                            ArtistId = 1,
                            FirstName = "Pablo",
                            LastName = "Picasso"
                        },
                        new
                        {
                            ArtistId = 2,
                            FirstName = "Dee",
                            LastName = "Bell"
                        },
                        new
                        {
                            ArtistId = 3,
                            FirstName = "Katharine",
                            LastName = "Kuharic"
                        });
                });

            modelBuilder.Entity("PublisherDomain.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            AuthorId = 1,
                            FirstName = "Rhoda",
                            LastName = "Lerman"
                        },
                        new
                        {
                            AuthorId = 2,
                            FirstName = "Ruth",
                            LastName = "Ozeki"
                        },
                        new
                        {
                            AuthorId = 3,
                            FirstName = "Sofia",
                            LastName = "Segovia"
                        },
                        new
                        {
                            AuthorId = 4,
                            FirstName = "Ursula K.",
                            LastName = "LeGuin"
                        },
                        new
                        {
                            AuthorId = 5,
                            FirstName = "Hugh",
                            LastName = "Howey"
                        },
                        new
                        {
                            AuthorId = 6,
                            FirstName = "Isabelle",
                            LastName = "Allende"
                        });
                });

            modelBuilder.Entity("PublisherDomain.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BookId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            AuthorId = 1,
                            BasePrice = 0m,
                            PublishDate = new DateTime(1989, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "In God's Ear"
                        },
                        new
                        {
                            BookId = 2,
                            AuthorId = 2,
                            BasePrice = 0m,
                            PublishDate = new DateTime(2013, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "A Tale For the Time Being"
                        },
                        new
                        {
                            BookId = 3,
                            AuthorId = 3,
                            BasePrice = 0m,
                            PublishDate = new DateTime(1969, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The left hand of Darkness"
                        });
                });

            modelBuilder.Entity("PublisherDomain.Cover", b =>
                {
                    b.Property<int>("CoverId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DesignIdeas")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("DigitalOnly")
                        .HasColumnType("INTEGER");

                    b.HasKey("CoverId");

                    b.HasIndex("BookId")
                        .IsUnique();

                    b.ToTable("Covers");

                    b.HasData(
                        new
                        {
                            CoverId = 1,
                            BookId = 3,
                            DesignIdeas = "How about a left hand in the dark?",
                            DigitalOnly = false
                        },
                        new
                        {
                            CoverId = 2,
                            BookId = 2,
                            DesignIdeas = "Should we put a clock?",
                            DigitalOnly = true
                        },
                        new
                        {
                            CoverId = 3,
                            BookId = 1,
                            DesignIdeas = "A big ear in the clocks?",
                            DigitalOnly = false
                        });
                });

            modelBuilder.Entity("PublisherDomain.CoverAssignment", b =>
                {
                    b.Property<int>("ArtistId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ArtistsArtistId");

                    b.Property<int>("CoverId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("CoversCoverId");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.HasKey("ArtistId", "CoverId");

                    b.HasIndex("CoverId");

                    b.ToTable("CoverAssignment");
                });

            modelBuilder.Entity("PublisherDomain.Book", b =>
                {
                    b.HasOne("PublisherDomain.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("PublisherDomain.Cover", b =>
                {
                    b.HasOne("PublisherDomain.Book", "Book")
                        .WithOne("Cover")
                        .HasForeignKey("PublisherDomain.Cover", "BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("PublisherDomain.CoverAssignment", b =>
                {
                    b.HasOne("PublisherDomain.Artist", null)
                        .WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PublisherDomain.Cover", null)
                        .WithMany()
                        .HasForeignKey("CoverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PublisherDomain.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("PublisherDomain.Book", b =>
                {
                    b.Navigation("Cover")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
