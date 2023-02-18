using BookWorldStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookWorldStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDBContext dbContext;
        public CategoryController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var result = await dbContext.categories.ToListAsync();
            return View("~/Views/Admin/Category/Index.cshtml",result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Admin/Category/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            dbContext.categories.Add(category);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            
            var result = dbContext.categories.Find(id);
            return View("~/Views/Admin/Category/Edit.cshtml",result);
        }

        [HttpPost]
        public IActionResult Edit(int id, Category Model)
        {
            var category = dbContext.categories.Find(id);
            if (category != null)
            {
                category.name = Model.name;
                dbContext.categories.Update(category);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Category category= dbContext.categories.Find(id);
            dbContext.categories.Remove(category);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
