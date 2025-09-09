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
    public class MhkOrdersController : Controller
    {
        private readonly MhkProject2Context _context;

        public MhkOrdersController(MhkProject2Context context)
        {
            _context = context;
        }

        // GET: MhkOrders
        public async Task<IActionResult> MhkIndex()
        {
            var mhkProject2Context = _context.MhkOrders.Include(m => m.MhkUser);
            return View(await mhkProject2Context.ToListAsync());
        }

        // GET: MhkOrders/Details/5
        public async Task<IActionResult> MhkDetails(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkOrder = await _context.MhkOrders
                .Include(m => m.MhkUser)
                .FirstOrDefaultAsync(m => m.MhkOrderId == mhkid);
            if (mhkOrder == null)
            {
                return NotFound();
            }

            return View(mhkOrder);
        }

        // GET: MhkOrders/Create
        public IActionResult MhkCreate()
        {
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId");
            return View();
        }

        // POST: MhkOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkCreate([Bind("MhkOrderId,MhkUserId,MhkOrderDate,MhkStatus")] MhkOrder mhkOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mhkOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MhkIndex));
            }
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId", mhkOrder.MhkUserId);
            return View(mhkOrder);
        }

        // GET: MhkOrders/Edit/5
        public async Task<IActionResult> MhkEdit(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkOrder = await _context.MhkOrders.FindAsync(mhkid);
            if (mhkOrder == null)
            {
                return NotFound();
            }
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId", mhkOrder.MhkUserId);
            return View(mhkOrder);
        }

        // POST: MhkOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkEdit(int mhkid, [Bind("MhkOrderId,MhkUserId,MhkOrderDate,MhkStatus")] MhkOrder mhkOrder)
        {
            if (mhkid != mhkOrder.MhkOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mhkOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MhkOrderExists(mhkOrder.MhkOrderId))
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
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId", mhkOrder.MhkUserId);
            return View(mhkOrder);
        }

        // GET: MhkOrders/Delete/5
        public async Task<IActionResult> MhkDelete(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkOrder = await _context.MhkOrders
                .Include(m => m.MhkUser)
                .FirstOrDefaultAsync(m => m.MhkOrderId == mhkid);
            if (mhkOrder == null)
            {
                return NotFound();
            }

            return View(mhkOrder);
        }

        // POST: MhkOrders/Delete/5
        [HttpPost, ActionName("MhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkDeleteConfirmed(int mhkid)
        {
            var mhkOrder = await _context.MhkOrders.FindAsync(mhkid);
            if (mhkOrder != null)
            {
                _context.MhkOrders.Remove(mhkOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MhkIndex));
        }

        private bool MhkOrderExists(int mhkid)
        {
            return _context.MhkOrders.Any(e => e.MhkOrderId == mhkid);
        }
    }
}
