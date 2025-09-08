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
        public async Task<IActionResult> mhkIndex()
        {
            var mhkProject2Context = _context.MhkProducts.Include(m => m.MhkCategory);
            return View(await mhkProject2Context.ToListAsync());
        }

        // GET: MhkProducts/mhkDetails/5
        public async Task<IActionResult> Details(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkProduct = await _context.MhkProducts
                .Include(m => m.MhkCategory)
                .FirstOrDefaultAsync(m => m.MhkProductId == mhkid);
            if (mhkProduct == null)
            {
                return NotFound();
            }

            return View(mhkProduct);
        }

        // GET: MhkProducts/mhkCreate
        public IActionResult mhkCreate()
        {
            ViewData["MhkCategoryId"] = new SelectList(_context.MhkCategories, "MhkCategoryId", "MhkCategoryId");
            return View();
        }

        // POST: MhkProducts/mhkCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkCreate([Bind("MhkProductId,MhkCategoryId,MhkName,MhkPrice,MhkDescription")] MhkProduct mhkProduct)
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
        public async Task<IActionResult> mhkEdit(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkProduct = await _context.MhkProducts.FindAsync(mhkid);
            if (mhkProduct == null)
            {
                return NotFound();
            }
            ViewData["MhkCategoryId"] = new SelectList(_context.MhkCategories, "MhkCategoryId", "MhkCategoryId", mhkProduct.MhkCategoryId);
            return View(mhkProduct);
        }

        // POST: MhkProducts/mhkEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkEdit(int mhkid, [Bind("MhkProductId,MhkCategoryId,MhkName,MhkPrice,MhkDescription")] MhkProduct mhkProduct)
        {
            if (mhkid != mhkProduct.MhkProductId)
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
                return RedirectToAction(nameof(mhkIndex));
            }
            ViewData["MhkCategoryId"] = new SelectList(_context.MhkCategories, "MhkCategoryId", "MhkCategoryId", mhkProduct.MhkCategoryId);
            return View(mhkProduct);
        }

        // GET: MhkProducts/mhkDelete/5
        public async Task<IActionResult> mhkDelete(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkProduct = await _context.MhkProducts
                .Include(m => m.MhkCategory)
                .FirstOrDefaultAsync(m => m.MhkProductId == mhkid);
            if (mhkProduct == null)
            {
                return NotFound();
            }

            return View(mhkProduct);
        }

        // POST: MhkProducts/mhkDelete/5
        [HttpPost, ActionName("mhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkDeleteConfirmed(int mhkid)
        {
            var mhkProduct = await _context.MhkProducts.FindAsync(mhkid);
            if (mhkProduct != null)
            {
                _context.MhkProducts.Remove(mhkProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(mhkIndex));
        }

        private bool MhkProductExists(int mhkid)
        {
            return _context.MhkProducts.Any(e => e.MhkProductId == mhkid);
        }
    }
}
