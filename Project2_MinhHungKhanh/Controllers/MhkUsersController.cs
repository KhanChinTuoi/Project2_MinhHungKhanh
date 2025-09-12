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
        public async Task<IActionResult> MhkIndex()
        {
            return View(await _context.MhkUsers.ToListAsync());
        }

        // GET: MhkUsers/MhkDetails/5
        public async Task<IActionResult> MhkDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MhkUser = await _context.MhkUsers
                .FirstOrDefaultAsync(m => m.MhkUserId == id);
            if (MhkUser == null)
            {
                return NotFound();
            }

            return View(MhkUser);
        }

        // GET: MhkUsers/MhkCreate
        public IActionResult MhkCreate()
        {
            return View();
        }

        // POST: MhkUsers/MhkCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkCreate([Bind("MhkUserId,MhkFullName,MhkEmail")] MhkUser MhkUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(MhkUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MhkIndex));
            }
            return View(MhkUser);
        }

        // GET: MhkUsers/MhkEdit/5
        public async Task<IActionResult> MhkEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MhkUser = await _context.MhkUsers.FindAsync(id);
            if (MhkUser == null)
            {
                return NotFound();
            }
            return View(MhkUser);
        }

        // POST: MhkUsers/MhkEdit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkEdit(int? id, [Bind("MhkUserId,MhkFullName,MhkEmail")] MhkUser MhkUser)
        {
            if (id != MhkUser.MhkUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(MhkUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MhkUserExists(MhkUser.MhkUserId))
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
            return View(MhkUser);
        }

        // GET: MhkUsers/MhkDelete/5
        public async Task<IActionResult> MhkDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MhkUser = await _context.MhkUsers
                .FirstOrDefaultAsync(m => m.MhkUserId == id);
            if (MhkUser == null)
            {
                return NotFound();
            }

            return View(MhkUser);
        }

        // POST: MhkUsers/MhkDelete/5
        [HttpPost, ActionName("MhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkDeleteConfirmed(int? id)
        {
            var MhkUser = await _context.MhkUsers.FindAsync(id);
            if (MhkUser != null)
            {
                _context.MhkUsers.Remove(MhkUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MhkIndex));
        }

        private bool MhkUserExists(int? id)
        {
            return _context.MhkUsers.Any(e => e.MhkUserId == id);
        }
    }
}
