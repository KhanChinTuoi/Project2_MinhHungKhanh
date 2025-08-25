using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2_MinhHungKhanh.Models;

namespace Project2_MinhHungKhanh.Controllers
{
    public class MhkProductsController : Controller
    {
        private readonly MhkProject2Context _context;

        public MhkProductsController(MhkProject2Context context)
        {
            _context = context;
        }

        // GET: MhkProducts
        public async Task<IActionResult> Index()
        {
            var mhkProject2Context = _context.MhkProducts.Include(m => m.MhkCategory);
            return View(await mhkProject2Context.ToListAsync());
        }

        // GET: MhkProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkProduct = await _context.MhkProducts
                .Include(m => m.MhkCategory)
                .FirstOrDefaultAsync(m => m.MhkProductId == id);
            if (mhkProduct == null)
            {
                return NotFound();
            }

            return View(mhkProduct);
        }

        // GET: MhkProducts/Create
        public IActionResult Create()
        {
            ViewData["MhkCategoryId"] = new SelectList(_context.MhkCategories, "MhkCategoryId", "MhkCategoryId");
            return View();
        }

        // POST: MhkProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MhkProductId,MhkCategoryId,MhkName,MhkPrice,MhkDescription")] MhkProduct mhkProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mhkProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MhkCategoryId"] = new SelectList(_context.MhkCategories, "MhkCategoryId", "MhkCategoryId", mhkProduct.MhkCategoryId);
            return View(mhkProduct);
        }

        // GET: MhkProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkProduct = await _context.MhkProducts.FindAsync(id);
            if (mhkProduct == null)
            {
                return NotFound();
            }
            ViewData["MhkCategoryId"] = new SelectList(_context.MhkCategories, "MhkCategoryId", "MhkCategoryId", mhkProduct.MhkCategoryId);
            return View(mhkProduct);
        }

        // POST: MhkProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MhkProductId,MhkCategoryId,MhkName,MhkPrice,MhkDescription")] MhkProduct mhkProduct)
        {
            if (id != mhkProduct.MhkProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mhkProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MhkProductExists(mhkProduct.MhkProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MhkCategoryId"] = new SelectList(_context.MhkCategories, "MhkCategoryId", "MhkCategoryId", mhkProduct.MhkCategoryId);
            return View(mhkProduct);
        }

        // GET: MhkProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkProduct = await _context.MhkProducts
                .Include(m => m.MhkCategory)
                .FirstOrDefaultAsync(m => m.MhkProductId == id);
            if (mhkProduct == null)
            {
                return NotFound();
            }

            return View(mhkProduct);
        }

        // POST: MhkProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mhkProduct = await _context.MhkProducts.FindAsync(id);
            if (mhkProduct != null)
            {
                _context.MhkProducts.Remove(mhkProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MhkProductExists(int id)
        {
            return _context.MhkProducts.Any(e => e.MhkProductId == id);
        }
    }
}
