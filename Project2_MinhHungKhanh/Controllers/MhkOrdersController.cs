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
            var MhkProject2Context = _context.MhkOrders.Include(m => m.MhkUser);
            return View(await MhkProject2Context.ToListAsync());
        }

        // GET: MhkOrders/MhkDetails/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MhkOrder = await _context.MhkOrders
                .Include(m => m.MhkUser)
                .FirstOrDefaultAsync(m => m.MhkOrderId == id);
            if (MhkOrder == null)
            {
                return NotFound();
            }

            return View(MhkOrder);
        }

        // GET: MhkOrders/Create
        public IActionResult MhkCreate()
        {
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId");
            return View();
        }

        // POST: MhkOrders/MhkCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkCreate([Bind("MhkOrderId,MhkUserId,MhkOrderDate,MhkStatus")] MhkOrder MhkOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(MhkOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MhkIndex));
            }
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId", MhkOrder.MhkUserId);
            return View(MhkOrder);
        }

        // GET: MhkOrders/Edit/5
        public async Task<IActionResult> MhkEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MhkOrder = await _context.MhkOrders.FindAsync(id);
            if (MhkOrder == null)
            {
                return NotFound();
            }
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId", MhkOrder.MhkUserId);
            return View(MhkOrder);
        }

        // POST: MhkOrders/MhkEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkEdit(int? id, [Bind("MhkOrderId,MhkUserId,MhkOrderDate,MhkStatus")] MhkOrder MhkOrder)
        {
            if (id != MhkOrder.MhkOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(MhkOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MhkOrderExists(MhkOrder.MhkOrderId))
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
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId", MhkOrder.MhkUserId);
            return View(MhkOrder);
        }

        // GET: MhkOrders/MhkDelete/5
        public async Task<IActionResult> MhkDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MhkOrder = await _context.MhkOrders
                .Include(m => m.MhkUser)
                .FirstOrDefaultAsync(m => m.MhkOrderId == id);
            if (MhkOrder == null)
            {
                return NotFound();
            }

            return View(MhkOrder);
        }

        // POST: MhkOrders/MhkDelete/5
        [HttpPost, ActionName("MhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var MhkOrder = await _context.MhkOrders.FindAsync(id);
            if (MhkOrder != null)
            {
                _context.MhkOrders.Remove(MhkOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MhkIndex));
        }

        private bool MhkOrderExists(int? id)
        {
            return _context.MhkOrders.Any(e => e.MhkOrderId == id);
        }
    }
}
