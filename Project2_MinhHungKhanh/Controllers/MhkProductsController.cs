using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2_MinhHungKhanh.Models;

namespace Project2_MinhHungKhanh.Controllers
{
    public class MhkProductsController : Controller
    {
        private readonly MhkProject2Context _context;

        public MhkProductsController(MhkProject2Context context)
        {
            _context = context;
        }

        // GET: MhkProducts
        public async Task<IActionResult> MhkIndex()
        {
            var products = _context.MhkProducts.Include(p => p.MhkCategory);
            return View(await products.ToListAsync());
        }

        // GET: MhkProducts/MhkDetails/5
        public async Task<IActionResult> MhkDetails(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.MhkProducts
                .Include(p => p.MhkCategory)
                .FirstOrDefaultAsync(m => m.MhkProductId == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // GET: MhkProducts/MhkCreate
        public IActionResult MhkCreate()
        {
            ViewData["MhkCategoryId"] = new SelectList(_context.MhkCategories, "MhkCategoryId", "MhkCategoryName");
            return View();
        }

        // POST: MhkProducts/MhkCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkCreate([Bind("MhkProductId,MhkCategoryId,MhkName,MhkPrice,MhkDescription")] MhkProduct mhkProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mhkProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MhkIndex));
            }
            ViewData["MhkCategoryId"] = new SelectList(_context.MhkCategories, "MhkCategoryId", "MhkCategoryName", mhkProduct.MhkCategoryId);
            return View(mhkProduct);
        }

        // GET: MhkProducts/MhkEdit/5
        public async Task<IActionResult> MhkEdit(int? id)
        {
            if (id == null) return NotFound();

            var mhkProduct = await _context.MhkProducts.FindAsync(id);
            if (mhkProduct == null) return NotFound();

            ViewData["MhkCategoryId"] = new SelectList(_context.MhkCategories, "MhkCategoryId", "MhkCategoryName", mhkProduct.MhkCategoryId);
            return View(mhkProduct);
        }

        // POST: MhkProducts/MhkEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkEdit(int id, [Bind("MhkProductId,MhkCategoryId,MhkName,MhkPrice,MhkDescription")] MhkProduct mhkProduct)
        {
            if (id != mhkProduct.MhkProductId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mhkProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MhkProductExists(mhkProduct.MhkProductId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(MhkIndex));
            }
            ViewData["MhkCategoryId"] = new SelectList(_context.MhkCategories, "MhkCategoryId", "MhkCategoryName", mhkProduct.MhkCategoryId);
            return View(mhkProduct);
        }

        // GET: MhkProducts/MhkDelete/5
        public async Task<IActionResult> MhkDelete(int? id)
        {
            if (id == null) return NotFound();

            var mhkProduct = await _context.MhkProducts
                .Include(p => p.MhkCategory)
                .FirstOrDefaultAsync(m => m.MhkProductId == id);

            if (mhkProduct == null) return NotFound();

            return View(mhkProduct);
        }

        // POST: MhkProducts/MhkDelete/5
        [HttpPost, ActionName("MhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkDeleteConfirmed(int id)
        {
            var mhkProduct = await _context.MhkProducts.FindAsync(id);
            if (mhkProduct != null)
            {
                _context.MhkProducts.Remove(mhkProduct);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(MhkIndex));
        }

        private bool MhkProductExists(int id)
        {
            return _context.MhkProducts.Any(e => e.MhkProductId == id);
        }
    }
}
