using RepositoryPattern.Models;

namespace RepositoryPattern.Repository
{
    public class MemoryBookRepository: IBookRepository
    {
        private List<Book> _books = new List<Book>();
        public MemoryBookRepository() { 
            _books.Add(new Book { BookId = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Price = 10.99m });
            _books.Add(new Book { BookId = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Price = 8.99m });
            _books.Add(new Book { BookId = 3, Title = "1984", Author = "George Orwell", Price = 9.99m });
        }
        public List<Book> GetAllBooks()
        {
            return _books;
        }
        public Book GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.BookId == id)!;
        }

        public void AddBook(Book book)
        {
            _books.Add(book);
        }
        public void DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                _books.Remove(book);
            }
        }

    }
}
