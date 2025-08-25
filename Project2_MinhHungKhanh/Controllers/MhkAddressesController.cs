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
    public class MhkAddressesController : Controller
    {
        private readonly MhkProject2Context _context;

        public MhkAddressesController(MhkProject2Context context)
        {
            _context = context;
        }

        // GET: MhkAddresses
        public async Task<IActionResult> Index()
        {
            var mhkProject2Context = _context.MhkAddresses.Include(m => m.MhkUser);
            return View(await mhkProject2Context.ToListAsync());
        }

        // GET: MhkAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkAddress = await _context.MhkAddresses
                .Include(m => m.MhkUser)
                .FirstOrDefaultAsync(m => m.MhkAddressId == id);
            if (mhkAddress == null)
            {
                return NotFound();
            }

            return View(mhkAddress);
        }

        // GET: MhkAddresses/Create
        public IActionResult Create()
        {
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId");
            return View();
        }

        // POST: MhkAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MhkAddressId,MhkUserId,MhkLine1")] MhkAddress mhkAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mhkAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId", mhkAddress.MhkUserId);
            return View(mhkAddress);
        }

        // GET: MhkAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkAddress = await _context.MhkAddresses.FindAsync(id);
            if (mhkAddress == null)
            {
                return NotFound();
            }
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId", mhkAddress.MhkUserId);
            return View(mhkAddress);
        }

        // POST: MhkAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MhkAddressId,MhkUserId,MhkLine1")] MhkAddress mhkAddress)
        {
            if (id != mhkAddress.MhkAddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mhkAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MhkAddressExists(mhkAddress.MhkAddressId))
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
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId", mhkAddress.MhkUserId);
            return View(mhkAddress);
        }

        // GET: MhkAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkAddress = await _context.MhkAddresses
                .Include(m => m.MhkUser)
                .FirstOrDefaultAsync(m => m.MhkAddressId == id);
            if (mhkAddress == null)
            {
                return NotFound();
            }

            return View(mhkAddress);
        }

        // POST: MhkAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mhkAddress = await _context.MhkAddresses.FindAsync(id);
            if (mhkAddress != null)
            {
                _context.MhkAddresses.Remove(mhkAddress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MhkAddressExists(int id)
        {
            return _context.MhkAddresses.Any(e => e.MhkAddressId == id);
        }
    }
}
