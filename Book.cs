namespace Library
{
    public class Book
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public int NumberOfPages { get; set; }
        public int PublishingYear { get; set; }
        public string Publisher { get; set; }

        public Book(string title, string description, string genre, string author, int numberOfPages, int publishingYear, string publisher)
        {
            Title = title;
            Description = description;
            Genre = genre;
            Author = author;
            NumberOfPages = numberOfPages;
            PublishingYear = publishingYear;
            Publisher = publisher;
        }
        public static int CompareByTitle(Book book1, Book book2)
        {
            return string.Compare(book1.Title, book2.Title, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
