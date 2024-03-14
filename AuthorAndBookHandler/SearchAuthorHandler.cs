namespace Library
{
    public class SearchAuthorHandler : BookHandler
    {
        private List<Book> _books; 

        public SearchAuthorHandler(List<Book> books)
        {
            _books = books;
        }
        protected override bool CanHandle(BookRequest request)
        {
            return !string.IsNullOrEmpty(request.Author);
        }
        protected override void ProcessRequest(BookRequest request)
        {
            var booksByAuthor = _books.Where(book => book.Author == request.Author);

            foreach (var book in booksByAuthor)
            {
                Console.WriteLine("Author: " + book.Author +"\n Book title: "+ book.Title + "\n");
            }
        }
    }
}
