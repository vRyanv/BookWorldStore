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
        public async Task<IActionResult> Edit(int id)
        {
            
            var result = await dbContext.categories.Where(cate=>cate.cate_id==id).ToListAsync();
            return View("~/Views/Admin/Category/Edit.cshtml",result);
        }

        [HttpPut]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                dbContext.categories.Update(category);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }


    }
}
