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
    public class MhkUsersController : Controller
    {
        private readonly MhkProject2Context _context;

        public MhkUsersController(MhkProject2Context context)
        {
            _context = context;
        }

        // GET: MhkUsers
        public async Task<IActionResult> mhkIndex()
        {
            return View(await _context.MhkUsers.ToListAsync());
        }

        // GET: MhkUsers/mhkDetails/5
        public async Task<IActionResult> Details(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkUser = await _context.MhkUsers
                .FirstOrDefaultAsync(m => m.MhkUserId == mhkid);
            if (mhkUser == null)
            {
                return NotFound();
            }

            return View(mhkUser);
        }

        // GET: MhkUsers/mhkCreate
        public IActionResult mhkCreate()
        {
            return View();
        }

        // POST: MhkUsers/mhkCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkCreate([Bind("MhkUserId,MhkFullName,MhkEmail")] MhkUser mhkUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mhkUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(mhkIndex));
            }
            return View(mhkUser);
        }

        // GET: MhkUsers/mhkEdit/5
        public async Task<IActionResult> mhkEdit(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkUser = await _context.MhkUsers.FindAsync(mhkid);
            if (mhkUser == null)
            {
                return NotFound();
            }
            return View(mhkUser);
        }

        // POST: MhkUsers/mhkEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkEdit(int mhkid, [Bind("MhkUserId,MhkFullName,MhkEmail")] MhkUser mhkUser)
        {
            if (mhkid != mhkUser.MhkUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mhkUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MhkUserExists(mhkUser.MhkUserId))
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
            return View(mhkUser);
        }

        // GET: MhkUsers/mhkDelete/5
        public async Task<IActionResult> mhkDelete(int? mhkid)
        {
            if (mhkid == null)
            {
                return NotFound();
            }

            var mhkUser = await _context.MhkUsers
                .FirstOrDefaultAsync(m => m.MhkUserId == mhkid);
            if (mhkUser == null)
            {
                return NotFound();
            }

            return View(mhkUser);
        }

        // POST: MhkUsers/mhkDelete/5
        [HttpPost, ActionName("mhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkDeleteConfirmed(int mhkid)
        {
            var mhkUser = await _context.MhkUsers.FindAsync(mhkid);
            if (mhkUser != null)
            {
                _context.MhkUsers.Remove(mhkUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MhkUserExists(int mhkid)
        {
            return _context.MhkUsers.Any(e => e.MhkUserId == mhkid);
        }
    }
}
