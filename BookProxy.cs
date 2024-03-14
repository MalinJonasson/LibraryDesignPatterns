namespace Library
{
    public class BookProxy
    {
        Book Book = null;
        DatabaseConnection FakeDb = new DatabaseConnection(); 
        public string BookTitle { get; set; }
        public string Author { get; set; }
       
        public BookProxy(DatabaseConnection fakeDb, string bookTitle, string author)
        {
            FakeDb = fakeDb;
            BookTitle = bookTitle;
            Author = author;
        }
        public string Description
        {
            get
            {
                Load();
                return Book.Description;
            }
            set
            {
                Load();
                Book.Description = value;
            }
        }
        public string Genre
        {
            get
            {
                Load();
                return Book.Genre;
            }
            set
            {
                Load();
                Book.Genre = value;
            }
        }
        public int PageCount
        {
            get
            {
                Load();
                return Book.NumberOfPages;
            }
            set
            {
                Load();
                Book.NumberOfPages = value;
            }
        }
        public int PublishingYear
        {
            get
            {
                Load();
                return Book.PublishingYear;
            }
            set
            {
                Load();
                Book.PublishingYear = value;
            }
        }
        public string Publisher
        {
            get
            {
                Load();
                return Book.Publisher;
            }
            set
            {
                Load();
                Book.Publisher = value;
            }
        }

        public void Load()
        {
            if (Book == null)
            {
                Book = FakeDb.GetBook(BookTitle);
            }
        }
    }
}
