using BookWorldStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWorldStore.Controllers
{
    public class SupplierController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Admin/Supplier/Index.cshtml");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("~/Views/Admin/Supplier/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View("~/Views/Admin/Supplier/Index.cshtml");
        }

        [HttpPut]
        public IActionResult Edit(Supplier supplier)
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
