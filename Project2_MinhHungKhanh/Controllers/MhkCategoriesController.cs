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
    public class MhkCategoriesController : Controller
    {
        private readonly MhkProject2Context _context;

        public MhkCategoriesController(MhkProject2Context context)
        {
            _context = context;
        }

        // GET: MhkCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.MhkCategories.ToListAsync());
        }

        // GET: MhkCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkCategory = await _context.MhkCategories
                .FirstOrDefaultAsync(m => m.MhkCategoryId == id);
            if (mhkCategory == null)
            {
                return NotFound();
            }

            return View(mhkCategory);
        }

        // GET: MhkCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MhkCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MhkCategoryId,MhkCategoryName")] MhkCategory mhkCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mhkCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mhkCategory);
        }

        // GET: MhkCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkCategory = await _context.MhkCategories.FindAsync(id);
            if (mhkCategory == null)
            {
                return NotFound();
            }
            return View(mhkCategory);
        }

        // POST: MhkCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MhkCategoryId,MhkCategoryName")] MhkCategory mhkCategory)
        {
            if (id != mhkCategory.MhkCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mhkCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MhkCategoryExists(mhkCategory.MhkCategoryId))
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
            return View(mhkCategory);
        }

        // GET: MhkCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkCategory = await _context.MhkCategories
                .FirstOrDefaultAsync(m => m.MhkCategoryId == id);
            if (mhkCategory == null)
            {
                return NotFound();
            }

            return View(mhkCategory);
        }

        // POST: MhkCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mhkCategory = await _context.MhkCategories.FindAsync(id);
            if (mhkCategory != null)
            {
                _context.MhkCategories.Remove(mhkCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MhkCategoryExists(int id)
        {
            return _context.MhkCategories.Any(e => e.MhkCategoryId == id);
        }
    }
}
