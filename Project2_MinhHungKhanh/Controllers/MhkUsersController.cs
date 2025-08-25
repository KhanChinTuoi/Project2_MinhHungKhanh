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
        public async Task<IActionResult> Index()
        {
            return View(await _context.MhkUsers.ToListAsync());
        }

        // GET: MhkUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkUser = await _context.MhkUsers
                .FirstOrDefaultAsync(m => m.MhkUserId == id);
            if (mhkUser == null)
            {
                return NotFound();
            }

            return View(mhkUser);
        }

        // GET: MhkUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MhkUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MhkUserId,MhkFullName,MhkEmail")] MhkUser mhkUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mhkUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mhkUser);
        }

        // GET: MhkUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkUser = await _context.MhkUsers.FindAsync(id);
            if (mhkUser == null)
            {
                return NotFound();
            }
            return View(mhkUser);
        }

        // POST: MhkUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MhkUserId,MhkFullName,MhkEmail")] MhkUser mhkUser)
        {
            if (id != mhkUser.MhkUserId)
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
                return RedirectToAction(nameof(Index));
            }
            return View(mhkUser);
        }

        // GET: MhkUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkUser = await _context.MhkUsers
                .FirstOrDefaultAsync(m => m.MhkUserId == id);
            if (mhkUser == null)
            {
                return NotFound();
            }

            return View(mhkUser);
        }

        // POST: MhkUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mhkUser = await _context.MhkUsers.FindAsync(id);
            if (mhkUser != null)
            {
                _context.MhkUsers.Remove(mhkUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MhkUserExists(int id)
        {
            return _context.MhkUsers.Any(e => e.MhkUserId == id);
        }
    }
}
