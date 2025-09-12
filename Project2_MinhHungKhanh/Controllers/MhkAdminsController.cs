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
    public class MhkAdminsController : Controller
    {
        private readonly MhkProject2Context _context;

        public MhkAdminsController(MhkProject2Context context)
        {
            _context = context;
        }

        // GET: MhkAdmins
        public async Task<IActionResult> MhkIndex()
        {
            return View(await _context.MhkAdmins.ToListAsync());
        }

        // GET: MhkAdmins/Details/5
        public async Task<IActionResult> MhkDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MhkAdmin = await _context.MhkAdmins
                .FirstOrDefaultAsync(m => m.MhkAdminId == id);
            if (MhkAdmin == null)
            {
                return NotFound();
            }

            return View(MhkAdmin);
        }

        // GET: MhkAdmins/Create
        public IActionResult MhkCreate()
        {
            return View();
        }

        // POST: MhkAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkCreate([Bind("MhkAdminId,MhkFullName,MhkPassword,MhkFullName,MhkRole")] MhkAdmin MhkAdmin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(MhkAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MhkIndex));
            }
            return View(MhkAdmin);
        }

        // GET: MhkAdmins/Edit/5
        public async Task<IActionResult> MhkEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MhkAdmin = await _context.MhkAdmins.FindAsync(id);
            if (MhkAdmin == null)
            {
                return NotFound();
            }
            return View(MhkAdmin);
        }

        // POST: MhkAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkEdit(int? id, [Bind("MhkAdminId,MhkFullName,MhkPassword,MhkFullName,MhkRole")] MhkAdmin MhkAdmin)
        {
            if (id != MhkAdmin.MhkAdminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(MhkAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MhkAdminExists(MhkAdmin.MhkAdminId))
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
            return View(MhkAdmin);
        }

        // GET: MhkAdmins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MhkAdmin = await _context.MhkAdmins
                .FirstOrDefaultAsync(m => m.MhkAdminId == id);
            if (MhkAdmin == null)
            {
                return NotFound();
            }

            return View(MhkAdmin);
        }

        // POST: MhkAdmins/Delete/5
        [HttpPost, ActionName("MhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var MhkAdmin = await _context.MhkAdmins.FindAsync(id);
            if (MhkAdmin != null)
            {
                _context.MhkAdmins.Remove(MhkAdmin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MhkIndex));
        }

        private bool MhkAdminExists(int? id)
        {
            return _context.MhkAdmins.Any(e => e.MhkAdminId == id);
        }
    }
}
