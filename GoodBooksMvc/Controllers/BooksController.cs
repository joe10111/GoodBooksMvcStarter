using GoodBooksMvc.DataAccess;
using GoodBooksMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace GoodBooksMvc.Controllers
{
    public class BooksController : Controller
    {
        private readonly GoodBooksMvcContext _context;

        public BooksController(GoodBooksMvcContext context)
        {
            _context = context;
        }

        [Route("/[controller]")]
        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            
            string compareList = HttpContext.Request.Cookies["CompareList"] ?? "";

            var compareIds = new HashSet<string>(compareList.Split(',', StringSplitOptions.RemoveEmptyEntries));

            var highlightedBooks = new List<Book>();

            var otherBooks = new List<Book>();

            foreach (var book in books)
            {
                if (compareIds.Contains(book.Id.ToString()))
                {
                    highlightedBooks.Add(book);
                }
                else
                {
                    otherBooks.Add(book);
                }
            }

            ViewBag.HighlightedBooks = highlightedBooks;
            ViewBag.OtherBooks = otherBooks;
            ViewBag.CompareList = compareIds;

            return View(books);
        }

        // Action to toggle compare mode
        public IActionResult ToggleCompare(int id)
        {
            string compareList = HttpContext.Request.Cookies["CompareList"] ?? "";
            var compareIds = new HashSet<string>(compareList.Split(',', StringSplitOptions.RemoveEmptyEntries));

            if (compareIds.Contains(id.ToString()))
            {
                compareIds.Remove(id.ToString());
            }
            else
            {
                compareIds.Add(id.ToString());
            }

            HttpContext.Response.Cookies.Append("CompareList", string.Join(',', compareIds));

            return RedirectToAction("Index");
        }
    }
}
