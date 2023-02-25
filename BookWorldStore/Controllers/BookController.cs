using BookWorldStore.Helper;
using BookWorldStore.Models;
using BookWorldStore.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Xml.Linq;

namespace BookWorldStore.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDBContext dbContext;

        public BookController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Authorize(Roles = "owner, admin")]
        public IActionResult Index()
        {
            var book = dbContext.books.Where(b => b.status == 1).Include("category").Include("supplier").ToList();
            return View("~/Views/Admin/Book/Index.cshtml",book);
        }

        [HttpGet]
        [Authorize(Roles = "owner, admin")]
        public async Task<IActionResult> Create()
        {
            var cateList = await dbContext.categories.Where(c => c.status == 1).ToListAsync();
            var supList = await dbContext.suppliers.Where(s => s.status == 1).ToListAsync();
            ViewData["cateList"] = cateList;
            ViewData["supList"] = supList;
            return View("~/Views/Admin/Book/Create.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "owner, admin")]
        public async Task<IActionResult> Create([FromForm] Book book, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                string folder = "img/book";
                string fileName = await FileHelper.Instance.SaveFileAsync(image, folder);
                book.image = fileName;
                book.status = 1;

                dbContext.Add(book);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        [HttpGet]
        [Authorize(Roles = "owner, admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Book book = await dbContext.books.FindAsync(id);
            var cateList = await dbContext.categories.Where(c => c.status == 1).ToListAsync();
            var supList = await dbContext.suppliers.Where(s => s.status == 1).ToListAsync();
            ViewData["cateList"] = cateList;
            ViewData["supList"] = supList;
            return View("~/Views/Admin/Book/Edit.cshtml", book);
        }

        [HttpPost]
        [Authorize(Roles = "owner, admin")]
        public async Task<IActionResult> Edit([FromForm] Book book, IFormFile? image)
        {
            if (ModelState.IsValid)
            {
                List<Book> updateBook = await dbContext.books
                                           .Where(b => b.status == 1 && b.book_id == book.book_id)
                                           .ToListAsync();
                if (updateBook.Count != 0)
                {
                    updateBook[0].title = book.title;
                    updateBook[0].author = book.author;
                    updateBook[0].des = book.des;
                    updateBook[0].price = book.price;
                    updateBook[0].cate_id = book.cate_id;
                    updateBook[0].sup_id = book.sup_id;
                    updateBook[0].inventory_num = book.inventory_num;
                    updateBook[0].publishing_year = book.publishing_year;

                    if(image != null)
                    {
                        string oldImg = updateBook[0].image;
                        string folder = "img/book";
                        FileHelper.Instance.DeleteFileAsync(oldImg, folder);
                        string fileName = await FileHelper.Instance.SaveFileAsync(image, folder);

                        updateBook[0].image = fileName;
                    }

                    dbContext.Update(updateBook[0]);
                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.error = "Error - Not found book";
            }
            else
            {
                ViewBag.error = "Error - Some field is invalid ";
            }

            var cateList = await dbContext.categories.Where(c => c.status == 1).ToListAsync();
            var supList = await dbContext.suppliers.Where(s => s.status == 1).ToListAsync();
            ViewData["cateList"] = cateList;
            ViewData["supList"] = supList;
            return View("~/Views/Admin/Book/Edit.cshtml", book);
        }

        [HttpPost]
        [Authorize(Roles = "owner, admin")]
        public async Task<IActionResult> Delete(int id)
        {
            List<Book> deleteBook = await dbContext.books
                           .Where(b => b.status == 1 && b.book_id == id)
                           .ToListAsync();
            if(deleteBook.Count > 0)
            {

                deleteBook[0].status = 0;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        [Authorize(Roles = "owner, admin")]
        public async Task<IActionResult> SearchBook([FromQuery(Name ="title")]string title)
        {
            ICollection<Book> bookList = await dbContext.books
                                            .Where(b => b.status == 1 && b.title.Contains(title))
                                            .Include(c => c.category).Include(s => s.supplier)
                                            .ToListAsync();
            return View("~/Views/Admin/Book/Index.cshtml", bookList);
        }

    }
}
