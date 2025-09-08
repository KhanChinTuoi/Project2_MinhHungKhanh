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
        public async Task<IActionResult> mhkIndex()
        {
            return View(await _context.MhkCategories.ToListAsync());
        }

        // GET: MhkCategories/mhkDetails/5
        public async Task<IActionResult> mhkDetails(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkCategory = await _context.MhkCategories
                .FirstOrDefaultAsync(m => m.MhkCategoryId == mhkid);
            if (mhkCategory == null)
            {
                return NotFound();
            }

            return View(mhkCategory);
        }

        // GET: MhkCategories/mhkCreate
        public IActionResult mhkCreate()
        {
            return View();
        }

        // POST: MhkCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkCreate([Bind("MhkCategoryId,MhkCategoryName")] MhkCategory mhkCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mhkCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(mhkIndex));
            }
            return View(mhkCategory);
        }

        // GET: MhkCategories/mhkEdit/5
        public async Task<IActionResult> mhkEdit(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkCategory = await _context.MhkCategories.FindAsync(mhkid);
            if (mhkCategory == null)
            {
                return NotFound();
            }
            return View(mhkCategory);
        }

        // POST: MhkCategories/mhkEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int mhkid, [Bind("MhkCategoryId,MhkCategoryName")] MhkCategory mhkCategory)
        {
            if (mhkid != mhkCategory.MhkCategoryId)
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
                return RedirectToAction(nameof(mhkIndex));
            }
            return View(mhkCategory);
        }

        // GET: MhkCategories/mhkDelete/5
        public async Task<IActionResult> mhkDelete(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkCategory = await _context.MhkCategories
                .FirstOrDefaultAsync(m => m.MhkCategoryId == mhkid);
            if (mhkCategory == null)
            {
                return NotFound();
            }

            return View(mhkCategory);
        }

        // POST: MhkCategories/mhkDelete/5
        [HttpPost, ActionName("mhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkDeleteConfirmed(int mhkid)
        {
            var mhkCategory = await _context.MhkCategories.FindAsync(mhkid);
            if (mhkCategory != null)
            {
                _context.MhkCategories.Remove(mhkCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(mhkIndex));
        }

        private bool MhkCategoryExists(int mhkid)
        {
            return _context.MhkCategories.Any(e => e.MhkCategoryId == mhkid);
        }
    }
}
