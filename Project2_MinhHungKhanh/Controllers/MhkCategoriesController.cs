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
        public async Task<IActionResult> MhkIndex()
        {
            return View(await _context.MhkCategories.ToListAsync());
        }

        // GET: MhkCategories/MhkDetails/5
        public async Task<IActionResult> MhkDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MhkCategory = await _context.MhkCategories
                .FirstOrDefaultAsync(m => m.MhkCategoryId == id);
            if (MhkCategory == null)
            {
                return NotFound();
            }

            return View(MhkCategory);
        }

        // GET: MhkCategories/MhkCreate
        public IActionResult MhkCreate()
        {
            return View();
        }

        // POST: MhkCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkCreate([Bind("MhkCategoryId,MhkCategoryName")] MhkCategory MhkCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(MhkCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MhkIndex));
            }
            return View(MhkCategory);
        }

        // GET: MhkCategories/MhkEdit/5
        public async Task<IActionResult> MhkEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MhkCategory = await _context.MhkCategories.FindAsync(id);
            if (MhkCategory == null)
            {
                return NotFound();
            }
            return View(MhkCategory);
        }

        // POST: MhkCategories/MhkEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkEdit(int? id, [Bind("MhkCategoryId,MhkCategoryName")] MhkCategory MhkCategory)
        {
            if (id != MhkCategory.MhkCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(MhkCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MhkCategoryExists(MhkCategory.MhkCategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MhkIndex));
            }
            return View(MhkCategory);
        }

        // GET: MhkCategories/MhkDelete/5
        public async Task<IActionResult> MhkDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MhkCategory = await _context.MhkCategories
                .FirstOrDefaultAsync(m => m.MhkCategoryId == id);
            if (MhkCategory == null)
            {
                return NotFound();
            }

            return View(MhkCategory);
        }

        // POST: MhkCategories/MhkDelete/5
        [HttpPost, ActionName("MhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkDeleteConfirmed(int? id)
        {
            var MhkCategory = await _context.MhkCategories.FindAsync(id);
            if (MhkCategory != null)
            {
                _context.MhkCategories.Remove(MhkCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MhkIndex));
        }

        private bool MhkCategoryExists(int? id)
        {
            return _context.MhkCategories.Any(e => e.MhkCategoryId == id);
        }
    }
}
