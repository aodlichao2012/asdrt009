using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using test3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace test3.Controllers
{
  
        public class CustomerController : Controller
    {
        private readonly DbEntity db;
        public CustomerController(DbEntity context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            var cs = db.Customers;
            return View(await cs.ToListAsync());
        }
        public async Task<IActionResult> Search(string txtQ)
        {

            if (string.IsNullOrEmpty(txtQ))
            {
                return View("Index", await db.Customers.ToListAsync());
            }
            else
            {
                return View("Index", await db.Customers.Where(c => c.Name.Contains(txtQ)).ToListAsync());
            }
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cs = await db.Customers.SingleOrDefaultAsync(c => c.ID == id);
            if (cs == null)
            {
                return NotFound();
            }
            return View(cs);
        }
        public IActionResult Create()
        {
            ViewData["Price"] = new SelectList(db.Customers, "ID", "Price", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customers c)
        {
            if (ModelState.IsValid)
            {
                db.Add(c);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            if (!ModelState.IsValid)
            {
                return View(c);
            }
            ViewData["Price"] = new SelectList(db.Customers, "Price", "Name", c.ID);
            return View(c);

        }

        public async Task<IActionResult> Delete(Customers d)
        {
                db.Remove(d);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var cs = await db.Customers.SingleOrDefaultAsync(c => c.ID == id);
            if(cs == null)
            {
                return NotFound();
            }
            ViewData["ID"] = new SelectList(db.Customers, "ID", "Name", "Price");
            return View(cs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customers c)
        {
            if(id != c.ID)
            {
                return NotFound();
            }
          
            if(ModelState.IsValid)
            {
                try
                {
                    db.Update(c);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!CheckCustomers(c.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ID"] = new SelectList(db.Customers, "ID", "Name", "Price", c.Name);
            return View(c);

        }
        private bool CheckCustomers(int id)
        {
            return db.Customers.Any(c => c.ID == id);
        }
        public async Task<IActionResult> Sale(int? id,Customers d)
        {

            if (id == null)
            {
                return NotFound();
            }
            var cs = await db.Customers.SingleOrDefaultAsync(c => c.ID == id);
            if (cs == null)
            {
                return NotFound();
            }
            db.UpdateRange(d);
            await db.SaveChangesAsync();
            return View(cs);
        }
        public async Task<IActionResult> Buy(int? id,Customers d)
        {

            if (id == null)
            {
                return NotFound();
            }
            var cs = await db.Customers.SingleOrDefaultAsync(c => c.ID == id);
            if (cs == null)
            {
                return NotFound();
            }
            return View(cs);
        }
    }
}
