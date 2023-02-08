using BookWorldStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWorldStore.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Admin/Category/Index.cshtml");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Admin/Category/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View("~/Views/Admin/Category/Edit.cshtml");
        }

        [HttpPut]
        public IActionResult Edit(Category category)
        {
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }


    }
}
