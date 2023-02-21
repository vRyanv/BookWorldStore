using BookWorldStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookWorldStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDBContext dbContext;
        public CategoryController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Authorize(Roles = "owner, admin")]
        public async Task<IActionResult> Index()
        {

            var result = await dbContext.categories.Where(c=>c.status==1 || c.status == 2).ToListAsync();
            return View("~/Views/Admin/Category/Index.cshtml",result);
        }

        [HttpGet]
        [Authorize(Roles = "owner, admin")]
        public IActionResult Create()
        {
            return View("~/Views/Admin/Category/Create.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "owner, admin")]
        public IActionResult Create(Category category)
        {
            category.status = 2;
            dbContext.categories.Add(category);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "owner, admin")]
        public IActionResult Edit(int id)
        {
            
            var result = dbContext.categories.Find(id);
            return View("~/Views/Admin/Category/Edit.cshtml",result);
        }

        [HttpPost]
        [Authorize(Roles = "owner, admin")]
        public IActionResult Edit(int id, Category Model)
        {
            var category = dbContext.categories.Where(c=>c.cate_id==id && c.status==1).FirstOrDefault();
            if (category != null)
            {
                category.name = Model.name;
                dbContext.categories.Update(category);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "owner, admin")]
        public async Task<IActionResult> Delete(int id)
        {

            Category category= await dbContext.categories.Where(c=>c.cate_id==id && c.status==1).FirstAsync();
            category.status = 0;
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
