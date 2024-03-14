namespace Library
{
    public class DatabaseConnection
    {
        // A fake database
        public List<Book> booksInLibrary = new List<Book> ();
        public List<Book> borrowedBooks = new List<Book>();

        public DatabaseConnection()
        {
            booksInLibrary.Add(new Book("Normal People","A story about Marianne and Connell","Love and drama", "Sally Rooney", 266, 2018, "Faber and Faber"));
            booksInLibrary.Add(new Book("Where the crawdads sing", "A woman who raised herself i the marshes becomes a suspect of a murder", "Mystery", "Delia Owens", 400, 2021, "Penguin Publishing Group"));
            booksInLibrary.Add(new Book("Pride and Prejudice", "A love story between Elizabeth Bennet and Fitzwilliam Darcy", "Love and satir", "Jane Austin", 470, 1813, "C.E. Brock"));
        }
        public void AddBook(Book book)
        {
            booksInLibrary.Add(book);
        }
        public void RemoveBook(Book book)
        {
            booksInLibrary.Remove(book);
        }
        public Book GetBook(string title)
        {
            foreach (var book in booksInLibrary)
            {
                if (book.Title == title)
                {
                    return book;
                }
            }
            return null;
        }
        public List<BookProxy> GetBorrowedBooks()
        {
            List<BookProxy> borrowedBooksProxy = new List<BookProxy>();

            foreach (var book in borrowedBooks)
            {
                borrowedBooksProxy.Add(new BookProxy(this, book.Title, book.Author));
            }
            return borrowedBooksProxy;
        }
        public List<BookProxy> GetProxys()
        {
            List<BookProxy> bookProxys = new List<BookProxy>();
            foreach (var book in booksInLibrary)
            {
                bookProxys.Add(new BookProxy(this, book.Title, book.Author));
            }
            return bookProxys;
        }

    } 
}
