using RepositoryPattern.Models;

namespace RepositoryPattern.Repository
{
    public class SqlBookRepository: IBookRepository
    {
        public LibraryDbContext LibraryDbContext { get; }
        public SqlBookRepository(LibraryDbContext libraryDbContext) {
            LibraryDbContext = libraryDbContext;
        }
        public void AddBook(Book book)
        {
            LibraryDbContext.Books.Add(book);
            LibraryDbContext.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                LibraryDbContext.Books.Remove(book);
                LibraryDbContext.SaveChanges();

            }
        }

        public List<Book> GetAllBooks()
        {
            var books = LibraryDbContext.Books.ToList();
            return books;
        }

        public Book GetBookById(int id)
        {
            var book= LibraryDbContext.Books.FirstOrDefault(b => b.BookId == id);
            return book!;
        }

        
    }
}
