using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Models;
using RepositoryPattern.Repository;
using System.Diagnostics;

namespace RepositoryPattern.Controllers
{
    public class BookController : Controller
    {
        
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository book)
        {
            _bookRepository = book;
        }

      
        public IActionResult Index()
        {
            var books = _bookRepository.GetAllBooks();
            return View(books);
        }
        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetBookById(id);
            return View(book);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            _bookRepository.AddBook(book);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _bookRepository.DeleteBook(id);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
