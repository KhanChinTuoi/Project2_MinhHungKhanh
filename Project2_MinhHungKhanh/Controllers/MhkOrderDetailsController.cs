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
    public class MhkOrderDetailsController : Controller
    {
        private readonly MhkProject2Context _context;

        public MhkOrderDetailsController(MhkProject2Context context)
        {
            _context = context;
        }

        // GET: MhkOrderDetails
        public async Task<IActionResult> MhkIndex()
        {
            var mhkProject2Context = _context.MhkOrderDetails.Include(m => m.MhkOrder).Include(m => m.MhkProduct);
            return View(await mhkProject2Context.ToListAsync());
        }

        // GET: MhkOrderDetails/Details/5
        public async Task<IActionResult> MhkDetails(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkOrderDetail = await _context.MhkOrderDetails
                .Include(m => m.MhkOrder)
                .Include(m => m.MhkProduct)
                .FirstOrDefaultAsync(m => m.MhkOrderDetailId == mhkid);
            if (mhkOrderDetail == null)
            {
                return NotFound();
            }

            return View(mhkOrderDetail);
        }

        // GET: MhkOrderDetails/Create
        public IActionResult MhkCreate()
        {
            ViewData["MhkOrderId"] = new SelectList(_context.MhkOrders, "MhkOrderId", "MhkOrderId");
            ViewData["MhkProductId"] = new SelectList(_context.MhkProducts, "MhkProductId", "MhkProductId");
            return View();
        }

        // POST: MhkOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkCreate([Bind("MhkOrderDetailId,MhkOrderId,MhkProductId,MhkQuantity,MhkUnitPrice")] MhkOrderDetail mhkOrderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mhkOrderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MhkIndex));
            }
            ViewData["MhkOrderId"] = new SelectList(_context.MhkOrders, "MhkOrderId", "MhkOrderId", mhkOrderDetail.MhkOrderId);
            ViewData["MhkProductId"] = new SelectList(_context.MhkProducts, "MhkProductId", "MhkProductId", mhkOrderDetail.MhkProductId);
            return View(mhkOrderDetail);
        }

        // GET: MhkOrderDetails/Edit/5
        public async Task<IActionResult> MhkEdit(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkOrderDetail = await _context.MhkOrderDetails.FindAsync(mhkid);
            if (mhkOrderDetail == null)
            {
                return NotFound();
            }
            ViewData["MhkOrderId"] = new SelectList(_context.MhkOrders, "MhkOrderId", "MhkOrderId", mhkOrderDetail.MhkOrderId);
            ViewData["MhkProductId"] = new SelectList(_context.MhkProducts, "MhkProductId", "MhkProductId", mhkOrderDetail.MhkProductId);
            return View(mhkOrderDetail);
        }

        // POST: MhkOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkEdit(int mhkid, [Bind("MhkOrderDetailId,MhkOrderId,MhkProductId,MhkQuantity,MhkUnitPrice")] MhkOrderDetail mhkOrderDetail)
        {
            if (mhkid != mhkOrderDetail.MhkOrderDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mhkOrderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MhkOrderDetailExists(mhkOrderDetail.MhkOrderDetailId))
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
            ViewData["MhkOrderId"] = new SelectList(_context.MhkOrders, "MhkOrderId", "MhkOrderId", mhkOrderDetail.MhkOrderId);
            ViewData["MhkProductId"] = new SelectList(_context.MhkProducts, "MhkProductId", "MhkProductId", mhkOrderDetail.MhkProductId);
            return View(mhkOrderDetail);
        }

        // GET: MhkOrderDetails/Delete/5
        public async Task<IActionResult> MhkDelete(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkOrderDetail = await _context.MhkOrderDetails
                .Include(m => m.MhkOrder)
                .Include(m => m.MhkProduct)
                .FirstOrDefaultAsync(m => m.MhkOrderDetailId == mhkid);
            if (mhkOrderDetail == null)
            {
                return NotFound();
            }

            return View(mhkOrderDetail);
        }

        // POST: MhkOrderDetails/Delete/5
        [HttpPost, ActionName("MhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int mhkid)
        {
            var mhkOrderDetail = await _context.MhkOrderDetails.FindAsync(mhkid);
            if (mhkOrderDetail != null)
            {
                _context.MhkOrderDetails.Remove(mhkOrderDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MhkIndex));
        }

        private bool MhkOrderDetailExists(int mhkid)
        {
            return _context.MhkOrderDetails.Any(e => e.MhkOrderDetailId == mhkid);
        }
    }
}
