using BookWorldStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookWorldStore.Controllers
{
    public class SupplierController : Controller
    {
        private readonly AppDBContext dbContext;
        public SupplierController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var result = await dbContext.suppliers.Where(s=>s.status==1).ToListAsync();
            return View("~/Views/Admin/Supplier/Index.cshtml", result);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View("~/Views/Admin/Supplier/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            dbContext.suppliers.Add(supplier);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var result = dbContext.suppliers.Find(id);
            return View("~/Views/Admin/Supplier/Edit.cshtml",result);
        }

        [HttpPost]
        public IActionResult Edit(int id,Supplier Model)
        {
            var supplier=dbContext.suppliers.Find(id);
            if (supplier != null)
            {
                supplier.name= Model.name;
                supplier.address= Model.address;
                dbContext.Update(supplier);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            Supplier supplier=dbContext.suppliers.Find(id);
            dbContext.Remove(supplier);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
