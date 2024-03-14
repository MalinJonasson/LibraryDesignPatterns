using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class SearchBookHandler : BookHandler
    {
        private List<Book> _books;

        public SearchBookHandler(List<Book> books)
        {
            _books = books;
        }
        protected override bool CanHandle(BookRequest request)
        {
            return !string.IsNullOrEmpty(request.BookTitle);
        }

        protected override void ProcessRequest(BookRequest request)
        {
            var searchBooks = _books.Where(book => book.Title == request.BookTitle);

            foreach (var book in searchBooks)
            {
                Console.WriteLine("Book Title: " + book.Title+ "\n Author: " + book.Author + "\n");
            }
        }
    }
}
