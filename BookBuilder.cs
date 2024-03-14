namespace Library
{
    public class BookBuilder
    {
        public string Title { get; set; } = "Unknown";
        public string Description { get; set; } = "None";
        public string Genre { get; set; } = "Unknown";
        public string Author { get; set; } = "Unknown";
        public int NumberOfPages { get; set; } = 200;
        public int PublishingYear { get; set; } = 2024;
        public string Publisher { get; set; } = "Bonnier";

        public BookBuilder SetTitle(string title)
        {
            Title = title;
            return this;
        }
        public BookBuilder SetDescription(string description)
        {
            Description = description;
            return this;
        }

        public BookBuilder SetGenre(string genre)
        {
            Genre = genre;
            return this;
        }

        public BookBuilder SetAuthor(string author)
        {
            Author = author;
            return this;
        }

        public BookBuilder SetNumberOfPages(int numberOfPages)
        {
            NumberOfPages = numberOfPages;
            return this;
        }

        public BookBuilder SetPublishingYear(int publishingYear)
        {
            PublishingYear = publishingYear;
            return this;
        }

        public BookBuilder SetPublisher (string publisher)
        {
            Publisher = publisher;
            return this;
        }

        public Book BuildBook() 
        {
            return new Book(Title, Description, Genre, Author, NumberOfPages, PublishingYear, Publisher);
        }

    }
}
