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
        public async Task<IActionResult> mhkIndex()
        {
            var mhkProject2Context = _context.MhkAddresses.Include(m => m.MhkUser);
            return View(await mhkProject2Context.ToListAsync());
        }

        // GET: MhkAddresses/Details/5
        public async Task<IActionResult> mhkDetails(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkAddress = await _context.MhkAddresses
                .Include(m => m.MhkUser)
                .FirstOrDefaultAsync(m => m.MhkAddressId == mhkid);
            if (mhkAddress == null)
            {
                return NotFound();
            }

            return View(mhkAddress);
        }

        // GET: MhkAddresses/Create
        public IActionResult mhkCreate()
        {
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId");
            return View();
        }

        // POST: MhkAddresses/mhkCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkCreate([Bind("MhkAddressId,MhkUserId,MhkLine1")] MhkAddress mhkAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mhkAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(mhkIndex));
            }
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId", mhkAddress.MhkUserId);
            return View(mhkAddress);
        }

        // GET: MhkAddresses/mhkEdit/5
        public async Task<IActionResult> mhkEdit(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkAddress = await _context.MhkAddresses.FindAsync(mhkid);
            if (mhkAddress == null)
            {
                return NotFound();
            }
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId", mhkAddress.MhkUserId);
            return View(mhkAddress);
        }

        // POST: MhkAddresses/mhkEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkEdit(int mhkid, [Bind("MhkAddressId,MhkUserId,MhkLine1")] MhkAddress mhkAddress)
        {
            if (mhkid != mhkAddress.MhkAddressId)
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
                return RedirectToAction(nameof(mhkIndex));
            }
            ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId", mhkAddress.MhkUserId);
            return View(mhkAddress);
        }

        // GET: MhkAddresses/mhkDelete/5
        public async Task<IActionResult> mhkDelete(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkAddress = await _context.MhkAddresses
                .Include(m => m.MhkUser)
                .FirstOrDefaultAsync(m => m.MhkAddressId == mhkid);
            if (mhkAddress == null)
            {
                return NotFound();
            }

            return View(mhkAddress);
        }

        // POST: MhkAddresses/mhkDelete/5
        [HttpPost, ActionName("mhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkDeleteConfirmed(int mhkid)
        {
            var mhkAddress = await _context.MhkAddresses.FindAsync(mhkid);
            if (mhkAddress != null)
            {
                _context.MhkAddresses.Remove(mhkAddress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(mhkIndex));
        }

        private bool MhkAddressExists(int mhkid)
        {
            return _context.MhkAddresses.Any(e => e.MhkAddressId == mhkid);
        }
    }
}
