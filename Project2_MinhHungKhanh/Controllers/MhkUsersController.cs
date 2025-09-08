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
        private readonly MhkProject2Context _mhkContext;

        public MhkUsersController(MhkProject2Context mhkContext)
        {
            _mhkContext = mhkContext;
        }

        // GET: MhkUsers
        public async Task<IActionResult> mhkIndex()
        {
            return View(await _mhkContext.MhkUsers.ToListAsync());
        }

        // GET: MhkUsers/Details/5
        public async Task<IActionResult> mhkDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkUser = await _mhkContext.MhkUsers
                .FirstOrDefaultAsync(m => m.MhkUserId == id);
            if (mhkUser == null)
            {
                return NotFound();
            }

            return View(mhkUser);
        }

        // GET: MhkUsers/Create
        public IActionResult mhkCreate()
        {
            return View();
        }

        // POST: MhkUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkCreate([Bind("MhkUserId,MhkFullName,MhkEmail")] MhkUser mhkUser)
        {
            if (ModelState.IsValid)
            {
                _mhkContext.Add(mhkUser);
                await _mhkContext.SaveChangesAsync();
                return RedirectToAction(nameof(mhkIndex));
            }
            return View(mhkUser);
        }

        // GET: MhkUsers/Edit/5
        public async Task<IActionResult> mhkEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkUser = await _mhkContext.MhkUsers.FindAsync(id);
            if (mhkUser == null)
            {
                return NotFound();
            }
            return View(mhkUser);
        }

        // POST: MhkUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkEdit(int id, [Bind("MhkUserId,MhkFullName,MhkEmail")] MhkUser mhkUser)
        {
            if (id != mhkUser.MhkUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _mhkContext.Update(mhkUser);
                    await _mhkContext.SaveChangesAsync();
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

        // GET: MhkUsers/Delete/5
        public async Task<IActionResult> mhkDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mhkUser = await _mhkContext.MhkUsers
                .FirstOrDefaultAsync(m => m.MhkUserId == id);
            if (mhkUser == null)
            {
                return NotFound();
            }

            return View(mhkUser);
        }

        // POST: MhkUsers/Delete/5
        [HttpPost, ActionName("mhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> mhkDeleteConfirmed(int id)
        {
            var mhkUser = await _mhkContext.MhkUsers.FindAsync(id);
            if (mhkUser != null)
            {
                _mhkContext.MhkUsers.Remove(mhkUser);
            }

            await _mhkContext.SaveChangesAsync();
            return RedirectToAction(nameof(mhkIndex));
        }

        private bool MhkUserExists(int id)
        {
            return _mhkContext.MhkUsers.Any(e => e.MhkUserId == id);
        }
    }
}
