using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            if (id == null) return NotFound();

            var mhkUser = await _context.MhkUsers
                .Include(u => u.MhkAddresses)
                .Include(u => u.MhkOrders)
                .FirstOrDefaultAsync(m => m.MhkUserId == id);

            if (mhkUser == null) return NotFound();

            return View(mhkUser);
        }

        // GET: MhkUsers/MhkCreate
        public IActionResult MhkCreate()
        {
            return View();
        }

        // POST: MhkUsers/MhkCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkCreate([Bind("MhkUserId,MhkFullName,MhkEmail")] MhkUser mhkUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mhkUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MhkIndex));
            }
            return View(mhkUser);
        }

        // GET: MhkUsers/MhkEdit/5
        public async Task<IActionResult> MhkEdit(int? id)
        {
            if (id == null) return NotFound();

            var mhkUser = await _context.MhkUsers.FindAsync(id);
            if (mhkUser == null) return NotFound();

            return View(mhkUser);
        }

        // POST: MhkUsers/MhkEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkEdit(int id, [Bind("MhkUserId,MhkFullName,MhkEmail")] MhkUser mhkUser)
        {
            if (id != mhkUser.MhkUserId) return NotFound();

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
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(MhkIndex));
            }
            return View(mhkUser);
        }

        // GET: MhkUsers/MhkDelete/5
        public async Task<IActionResult> MhkDelete(int? id)
        {
            if (id == null) return NotFound();

            var mhkUser = await _context.MhkUsers
                .FirstOrDefaultAsync(m => m.MhkUserId == id);

            if (mhkUser == null) return NotFound();

            return View(mhkUser);
        }

        // POST: MhkUsers/MhkDelete/5
        [HttpPost, ActionName("MhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkDeleteConfirmed(int id)
        {
            var mhkUser = await _context.MhkUsers.FindAsync(id);
            if (mhkUser != null)
            {
                _context.MhkUsers.Remove(mhkUser);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(MhkIndex));
        }

        private bool MhkUserExists(int id)
        {
            return _context.MhkUsers.Any(e => e.MhkUserId == id);
        }
    }
}
