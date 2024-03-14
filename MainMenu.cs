namespace Library
{
    public class MainMenu
    {
        DatabaseConnection fakeDb = new DatabaseConnection();
        List<BookProxy> bookList = new List<BookProxy>();
        List<BookProxy> borrowedBooks = new List<BookProxy>();
        QuickSort sort = new QuickSort();

        public MainMenu()
        {
            bookList = fakeDb.GetProxys();
            borrowedBooks= fakeDb.GetBorrowedBooks();
        }
        public void Run()
        {
            bool continuing = true;
            while (continuing == true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to the Library!\n");
                    Console.WriteLine("What do you want to do?\n");
                    Console.WriteLine("1. View books in Library");
                    Console.WriteLine("2. Borrow a book");
                    Console.WriteLine("3. Return your book");
                    Console.WriteLine("4. View list of your borrowed books");
                    Console.WriteLine("5. Add a book to the Library");
                    Console.WriteLine("6. Remove a book from the Library");
                    Console.WriteLine("7. Search books or authors");
                    Console.WriteLine("8. Sort books in alphabetical order\n");
                    MenuChoices();
                }
                catch
                {
                    Console.WriteLine("You can only write numbers");
                    Console.ReadKey();
                }
            }
        }
        public void MenuChoices()
        { 
            bool continuing = true;

            while (continuing == true)
            {
                int choiceInput = int.Parse(Console.ReadLine());
                switch (choiceInput)
                {
                    case 1:
                        Console.Clear();
                        PrintBookInfo();
                        Console.ReadKey();
                        continuing = false;
                        break;
                    case 2:
                        Console.Clear();
                        BorrowBook();
                        continuing = false;
                        break;
                    case 3:
                        Console.Clear();
                        borrowedBooks = fakeDb.GetBorrowedBooks();
                        ReturnBook();
                        continuing = false;
                        break;
                    case 4:
                        Console.Clear();
                        borrowedBooks = fakeDb.GetBorrowedBooks();
                        PrintBorrowedBookInfo();
                        Console.ReadKey();
                        continuing = false;
                        break;
                    case 5:
                        Console.Clear();
                        AddBook();
                        continuing = false;
                        break;
                    case 6:
                        Console.Clear();
                        RemoveBook();
                        continuing = false;
                        break;
                    case 7:
                        MatchingAuthorOrBook(); 
                        continuing = false;
                        break;
                    case 8:
                        Console.Clear();
                        sort.SortBooksAlphabetically(fakeDb.booksInLibrary);
                        sort.DisplaySortedBooks(fakeDb.booksInLibrary);
                        Console.ReadKey();
                        continuing = false;
                        break;
                }
            }
        }

        public void MatchingAuthorOrBook()
        {
            bool continuing = true;
            while (continuing)
            {
                Console.Clear();
                Console.WriteLine("Enter the author's or book's name:");
                string searchInput = Console.ReadLine();
                Console.Clear();

                var authorHandler = new SearchAuthorHandler(fakeDb.booksInLibrary);
                var bookHandler = new SearchBookHandler(fakeDb.booksInLibrary);

                // Lägg till BooksHandler och AuthorHandler i kedjan
                authorHandler.SetNextHandler(bookHandler);

                // Skapa en bokrequest för att söka efter författare
                var authorRequest = new BookRequest { Author = searchInput };

                // Skapa en bokrequest för att söka efter boktitel
                var bookRequest = new BookRequest { BookTitle = searchInput };

                foreach (var book in fakeDb.booksInLibrary)
                {
                    if (searchInput == book.Title || searchInput == book.Author)
                    {
                        authorHandler.HandleRequest(authorRequest);
                        bookHandler.HandleRequest(bookRequest);
                        Console.ReadKey();
                        continuing = false;
                        break;
                    }
                }
                if (continuing)
                {
                    Console.WriteLine("There are no books or authors by that name. Try again.");
                    Console.ReadKey();
                }

                continuing = false;
            }
        }

        public void Input(out string titleInput, out string descriptionInput, out string genreInput, out string authorInput, out int numOfPagesInput, out int publishingYearInput, out string publisherInput)
        {
            Console.WriteLine("Write the title of the book you want to add");
            titleInput = Console.ReadLine();

            Console.WriteLine("Write a short description of the book");
            descriptionInput = Console.ReadLine();

            Console.WriteLine("Write the genre of the book you want to add");
            genreInput = Console.ReadLine();

            Console.WriteLine("Write the author of the book");
            authorInput = Console.ReadLine();

            Console.WriteLine("Write the number of pages of the book");
            int.TryParse(Console.ReadLine(), out numOfPagesInput);

            Console.WriteLine("What year was the book published?");
            int.TryParse(Console.ReadLine(), out publishingYearInput);

            Console.WriteLine("Who published the book?");
            publisherInput = Console.ReadLine();
        }
        public void AddBook()
        {
            string titleInput, descriptionInput, genreInput, authorInput, publisherInput;
            int numOfPagesInput, publishingYearInput;

            Input(out titleInput, out descriptionInput, out genreInput, out authorInput, out numOfPagesInput, out publishingYearInput, out publisherInput);

            BookBuilder bookBuilder = new BookBuilder();

            if (string.IsNullOrEmpty(titleInput))
                titleInput = bookBuilder.Title;
            if (string.IsNullOrEmpty(descriptionInput))
                descriptionInput = bookBuilder.Description;
            if (string.IsNullOrEmpty(genreInput))
                genreInput = bookBuilder.Genre;
            if (string.IsNullOrEmpty(authorInput))
                authorInput = bookBuilder.Author;
            if (numOfPagesInput <= 0)
                numOfPagesInput = bookBuilder.NumberOfPages;
            if (publishingYearInput <= 0)
                publishingYearInput = bookBuilder.PublishingYear;
            if (string.IsNullOrEmpty(publisherInput))
                publisherInput = bookBuilder.Publisher;

            Book newBook = bookBuilder.SetTitle(titleInput)
                                        .SetDescription(descriptionInput)
                                        .SetGenre(genreInput)
                                        .SetAuthor(authorInput)
                                        .SetNumberOfPages(numOfPagesInput)
                                        .SetPublishingYear(publishingYearInput)
                                        .SetPublisher(publisherInput)
                                        .BuildBook();

            fakeDb.AddBook(newBook);
            bookList = fakeDb.GetProxys();

            Console.Clear();
            Console.WriteLine("You've added " + newBook.Title + " to the library");
            Console.ReadKey();
        }

        public void RemoveBook()
        {
            PrintBookTitle();

            Console.WriteLine("Write the title of the book you want to remove");
            string removeBookInput = Console.ReadLine();

            if (!string.IsNullOrEmpty(removeBookInput))
            {
                Book bookToRemove = fakeDb.booksInLibrary.FirstOrDefault(book => book.Title.Equals(removeBookInput, StringComparison.OrdinalIgnoreCase));

                if (bookToRemove != null)
                {
                    fakeDb.RemoveBook(bookToRemove);
                    Console.Clear();
                    Console.WriteLine(bookToRemove.Title + " has been removed from the library.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Invalid input. Please enter a valid book title.");
                Console.ReadKey();
            }
        }

        public void BorrowBook()
        {
            Console.WriteLine("Here are the books you can borrow\n");
            PrintBookTitle();

            Console.WriteLine("\nWrite the title of the book you want to borrow");
            string borrowBookInput = Console.ReadLine();
            if (!string.IsNullOrEmpty(borrowBookInput))
            {
                Book bookToBorrow = fakeDb.booksInLibrary.FirstOrDefault(book => book.Title.Equals(borrowBookInput, StringComparison.OrdinalIgnoreCase));

                if (bookToBorrow != null)
                {
                    fakeDb.borrowedBooks.Add(bookToBorrow);
                    fakeDb.booksInLibrary.Remove(bookToBorrow);
                    bookList = fakeDb.GetProxys();

                    Console.Clear();
                    Console.WriteLine("You borrowed " + bookToBorrow.Title);
                    Console.ReadKey();
                }
            }
        }

        public void ReturnBook()
        {
            Console.WriteLine("Here are your borrowed books:\n");

            if (fakeDb.borrowedBooks.Count > 0)
            {
                PrintBorrowedBookInfo();

                Console.WriteLine("Write the title of the book you want to return");
                string returnBookInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(returnBookInput))
                {
                    Book bookToReturn = fakeDb.borrowedBooks.FirstOrDefault(book => book.Title.Equals(returnBookInput, StringComparison.OrdinalIgnoreCase));

                    if (bookToReturn != null)
                    {
                        fakeDb.booksInLibrary.Add(bookToReturn);
                        fakeDb.borrowedBooks.Remove(bookToReturn);
                        Console.Clear();
                        Console.WriteLine("You returned " + bookToReturn.Title);
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("You currently have no borrowed books to return.");
                Console.ReadKey();
            }
        }
        public void PrintBookInfo()
        {
            foreach (BookProxy book in bookList)
            {
                Console.WriteLine("Book title: " + book.BookTitle);
                Console.WriteLine("Author: " + book.Author);
                Console.WriteLine("Genre: " + book.Genre);
                Console.WriteLine("Description: " + book.Description);
                Console.WriteLine("Page count: " + book.PageCount);
                Console.WriteLine("Publishing Year: " + book.PublishingYear);
                Console.WriteLine("Publisher: " + book.Publisher + "\n");
            }
        }
        public void PrintBookTitle()
        {
            foreach (BookProxy book in bookList)
            {
                Console.WriteLine("Book title: " + book.BookTitle);
            }
        }

        public void PrintBorrowedBookInfo()
        {
            foreach (BookProxy book in borrowedBooks)
            {
                Console.WriteLine("Book title " + book.BookTitle);
                Console.WriteLine("TAuthor: " + book.Author + "\n");
            }
        }

    }
}
