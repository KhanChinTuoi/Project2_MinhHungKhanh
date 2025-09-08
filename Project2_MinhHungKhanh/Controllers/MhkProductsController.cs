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
        private readonly MhkProject2Context _mhkContext;

        public MhkProductsController(MhkProject2Context mhkContext)
        {
            _mhkContext = mhkContext;
        }

        // GET: MhkProducts
        public async Task<IActionResult> mhkIndex()
        {
            var mhkProject2Context = _mhkContext.MhkProducts.Include(m => m.MhkCategory);
            return View(await mhkProject2Context.ToListAsync());
        }

        // GET: MhkProducts/Details/5
        public async Task<IActionResult> mhkDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkProduct = await _mhkContext.MhkProducts
                .Include(m => m.MhkCategory)
                .FirstOrDefaultAsync(m => m.MhkProductId == id);
            if (mhkProduct == null)
            {
                return NotFound();
            }

            return View(mhkProduct);
        }

        // GET: MhkProducts/Create
        public IActionResult mhkCreate()
        {
            ViewData["MhkCategoryId"] = new SelectList(_mhkContext.MhkCategories, "MhkCategoryId", "MhkCategoryId");
            return View();
        }

        // POST: MhkProducts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkCreate([Bind("MhkProductId,MhkName,MhkPrice,MhkDescription,MhkCategoryId")] MhkProduct mhkProduct)
        {
            if (ModelState.IsValid)
            {
                _mhkContext.Add(mhkProduct);
                await _mhkContext.SaveChangesAsync();
                return RedirectToAction(nameof(mhkIndex));
            }
            ViewData["MhkCategoryId"] = new SelectList(_mhkContext.MhkCategories, "MhkCategoryId", "MhkCategoryId", mhkProduct.MhkCategoryId);
            return View(mhkProduct);
        }

        // GET: MhkProducts/Edit/5
        public async Task<IActionResult> mhkEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkProduct = await _mhkContext.MhkProducts.FindAsync(id);
            if (mhkProduct == null)
            {
                return NotFound();
            }
            ViewData["MhkCategoryId"] = new SelectList(_mhkContext.MhkCategories, "MhkCategoryId", "MhkCategoryId", mhkProduct.MhkCategoryId);
            return View(mhkProduct);
        }

        // POST: MhkProducts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkEdit(int id, [Bind("MhkProductId,MhkName,MhkPrice,MhkDescription,MhkCategoryId")] MhkProduct mhkProduct)
        {
            if (id != mhkProduct.MhkProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _mhkContext.Update(mhkProduct);
                    await _mhkContext.SaveChangesAsync();
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
            ViewData["MhkCategoryId"] = new SelectList(_mhkContext.MhkCategories, "MhkCategoryId", "MhkCategoryId", mhkProduct.MhkCategoryId);
            return View(mhkProduct);
        }

        // GET: MhkProducts/Delete/5
        public async Task<IActionResult> mhkDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkProduct = await _mhkContext.MhkProducts
                .Include(m => m.MhkCategory)
                .FirstOrDefaultAsync(m => m.MhkProductId == id);
            if (mhkProduct == null)
            {
                return NotFound();
            }

            return View(mhkProduct);
        }

        // POST: MhkProducts/Delete/5
        [HttpPost, ActionName("mhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkDeleteConfirmed(int id)
        {
            var mhkProduct = await _mhkContext.MhkProducts.FindAsync(id);
            if (mhkProduct != null)
            {
                _mhkContext.MhkProducts.Remove(mhkProduct);
            }

            await _mhkContext.SaveChangesAsync();
            return RedirectToAction(nameof(mhkIndex));
        }

        private bool MhkProductExists(int id)
        {
            return _mhkContext.MhkProducts.Any(e => e.MhkProductId == id);
        }
    }
}
