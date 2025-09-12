using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2_MinhHungKhanh.Models;

public class MhkAddressesController : Controller
{
    private readonly MhkProject2Context _context;

    public MhkAddressesController(MhkProject2Context context)
    {
        _context = context;
    }

    // GET: MhkAddresses
    public async Task<IActionResult> MhkIndex()
    {
        var addresses = await _context.MhkAddresses
            .Include(a => a.MhkUser)
            .ToListAsync();
        return View(addresses);
    }

    // GET: MhkAddresses/MhkDetails/5
    public async Task<IActionResult> MhkDetails(int? id)
    {
        if (id == null) return NotFound();

        var MhkAddress = await _context.MhkAddresses
            .Include(a => a.MhkUser)
            .FirstOrDefaultAsync(m => m.MhkAddressId == id);

        if (MhkAddress == null) return NotFound();

        return View(MhkAddress);
    }

    // GET: MhkAddresses/MhkCreate
    public IActionResult MhkCreate()
    {
        return View();
    }

    // POST: MhkAddresses/MhkCreate
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MhkCreate([Bind("MhkUserId,MhkLine1")] MhkAddress MhkAddress)
    {
        if (ModelState.IsValid)
        {
            _context.Add(MhkAddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MhkIndex)); // ✅ sẽ quay về Index
        }

        return View(MhkAddress);
    }





    // GET: MhkAddresses/MhkEdit/5
    public async Task<IActionResult> MhkEdit(int? id)
    {
        if (id == null) return NotFound();

        var MhkAddress = await _context.MhkAddresses.FindAsync(id);
        if (MhkAddress == null) return NotFound();

        ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId", MhkAddress.MhkUserId);
        return View(MhkAddress);
    }

    // POST: MhkAddresses/MhkEdit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MhkEdit(int? id, [Bind("MhkAddressId,MhkUserId,MhkLine1")] MhkAddress MhkAddress)
    {
        if (id != MhkAddress.MhkAddressId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(MhkAddress);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MhkAddressExists(MhkAddress.MhkAddressId)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(MhkIndex));
        }
        ViewData["MhkUserId"] = new SelectList(_context.MhkUsers, "MhkUserId", "MhkUserId", MhkAddress.MhkUserId);
        return View(MhkAddress);
    }

    // GET: MhkAddresses/MhkDelete/5
    public async Task<IActionResult> MhkDelete(int? id)
    {
        if (id == null) return NotFound();

        var MhkAddress = await _context.MhkAddresses
            .Include(a => a.MhkUser)
            .FirstOrDefaultAsync(m => m.MhkAddressId == id);

        if (MhkAddress == null) return NotFound();

        return View(MhkAddress);
    }

    // POST: MhkAddresses/MhkDelete/5
    [HttpPost, ActionName("MhkDelete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var MhkAddress = await _context.MhkAddresses.FindAsync(id);
        if (MhkAddress != null)
        {
            _context.MhkAddresses.Remove(MhkAddress);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(MhkIndex));
    }

    private bool MhkAddressExists(int id)
    {
        return _context.MhkAddresses.Any(e => e.MhkAddressId == id);
    }
}
