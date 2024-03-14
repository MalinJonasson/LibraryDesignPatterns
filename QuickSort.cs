namespace Library
{
    public class QuickSort
    {
        public void QuickSortBooks(List<Book> books, int left, int right)
        {
            if (left < right)
            {
                int partitionIndex = Partition(books, left, right);

                // Sortera på vänster och höger sida om partitionen
                QuickSortBooks(books, left, partitionIndex - 1);
                QuickSortBooks(books, partitionIndex + 1, right);
            }
        }

        private int Partition(List<Book> books, int left, int right)
        {
            Book pivot = books[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (Book.CompareByTitle(books[j], pivot) <= 0)
                {
                    i++;

                    // Byt plats på böckerna
                    Book temp = books[i];
                    books[i] = books[j];
                    books[j] = temp;
                }
            }

            // Byt plats på pivot och elementet på position i+1
            Book tempPivot = books[i + 1];
            books[i + 1] = books[right];
            books[right] = tempPivot;

            return i + 1;
        }

        public void SortBooksAlphabetically(List<Book> books)
        {
            // Anropa QuickSort-metoden för att sortera böckerna
            QuickSortBooks(books, 0, books.Count - 1);
        }
        public void DisplaySortedBooks(List<Book> books)
        {
            Console.WriteLine("Books sorted alphabetically: \n"); ;
            foreach (var book in books)
            {
                Console.WriteLine(book.Title + " by " + book.Author);
            }
            Console.ReadKey();
        }
    }
}