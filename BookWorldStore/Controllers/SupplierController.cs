using BookWorldStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookWorldStore.Controllers
{
    public class SupplierController : Controller
    {
        private readonly AppDBContext dbContext;
        public SupplierController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Authorize(Roles = "owner, admin")]
        public async Task<IActionResult> Index()
        {
            var result = await dbContext.suppliers.Where(s => s.status == 1).ToListAsync();
            return View("~/Views/Admin/Supplier/Index.cshtml", result);
        }

        [HttpGet]
        [Authorize(Roles = "owner, admin")]
        public IActionResult Create()
        {

            return View("~/Views/Admin/Supplier/Create.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "owner, admin")]
        public IActionResult Create(Supplier supplier)
        {
            supplier.status = 1;
            dbContext.suppliers.Add(supplier);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "owner, admin")]
        public IActionResult Edit(int id)
        {
            var result = dbContext.suppliers.Where(s => s.status == 1 && s.sup_id == id).FirstOrDefault();
            return View("~/Views/Admin/Supplier/Edit.cshtml", result);
        }

        [HttpPost]
        [Authorize(Roles = "owner, admin")]
        public async Task<IActionResult> Edit(int id, Supplier Model)
        {
            var supplier = await dbContext.suppliers.Where(s => s.status == 1 && s.sup_id == id).FirstOrDefaultAsync();
            if (supplier != null)
            {
                supplier.name = Model.name;
                supplier.address = Model.address;
                dbContext.Update(supplier);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        [Authorize(Roles = "owner, admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Supplier supplier = await dbContext.suppliers.Where(s => s.status == 1 && s.sup_id == id).FirstOrDefaultAsync();
            supplier.status = 0;
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
