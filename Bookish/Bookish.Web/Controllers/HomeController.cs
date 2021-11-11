using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookish.DataAccess;

namespace Bookish.Web.Controllers
{
    public class HomeController : Controller
    {
        public BookRepository bookrepo = new BookRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Catalogue(string user_name = null)
        {
            string searchQuery = HttpContext.Request.QueryString["query"];
            IEnumerable<Book> library;
            if (user_name != null)
            {
                ViewBag.Title = user_name + "'s Catalogue";
                library = bookrepo.GetCopiesBorrowedByUser(bookrepo.GetUseridByName(User.Identity.Name)).GroupBy(o => o.id).Select(o => o.First());
            }
            else
            {
                ViewBag.Title = "Catalogue";
                if (searchQuery != null)
                {
                    library = bookrepo.searchBooks(searchQuery);
                } 
                else
                {
                    library = bookrepo.GetAllBooks();
                }
            }

            return View(library);
        }

        public ActionResult Book(int id)
        {
            var book = bookrepo.GetBook(id);
            ViewBag.Message = "Number of available copies " + bookrepo.GetNumberofAvailableCopies(id);
            return View(book);
        }

        public ActionResult NewBook()
        {
            ViewBag.Message = "Add a new book";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}