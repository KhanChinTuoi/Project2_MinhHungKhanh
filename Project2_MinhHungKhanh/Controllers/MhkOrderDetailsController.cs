using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project2_MinhHungKhanh.Models;

namespace Project2_MinhHungKhanh.Controllers
{
    public class MhkOrderDetailsController : Controller
    {
        private readonly MhkProject2Context _context;

        public MhkOrderDetailsController(MhkProject2Context context)
        {
            _context = context;
        }

        // GET: MhkOrderDetails
        public async Task<IActionResult> MhkIndex()
        {
            var orderDetails = _context.MhkOrderDetails
                                       .Include(o => o.MhkOrder)
                                       .Include(o => o.MhkProduct);
            return View(await orderDetails.ToListAsync());
        }

        // GET: MhkOrderDetails/MhkDetails/5
        public async Task<IActionResult> MhkDetails(int? id)
        {
            if (id == null) return NotFound();

            var orderDetail = await _context.MhkOrderDetails
                .Include(o => o.MhkOrder)
                .Include(o => o.MhkProduct)
                .FirstOrDefaultAsync(m => m.MhkOrderDetailId == id);

            if (orderDetail == null) return NotFound();

            return View(orderDetail);
        }

        // GET: MhkOrderDetails/MhkCreate
        public IActionResult MhkCreate()
        {
            ViewData["MhkOrderId"] = new SelectList(_context.MhkOrders, "MhkOrderId", "MhkOrderId");
            ViewData["MhkProductId"] = new SelectList(_context.MhkProducts, "MhkProductId", "MhkName");
            return View();
        }

        // POST: MhkOrderDetails/MhkCreate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkCreate([Bind("MhkOrderDetailId,MhkOrderId,MhkProductId,MhkQuantity,MhkUnitPrice")] MhkOrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MhkIndex));
            }
            ViewData["MhkOrderId"] = new SelectList(_context.MhkOrders, "MhkOrderId", "MhkOrderId", orderDetail.MhkOrderId);
            ViewData["MhkProductId"] = new SelectList(_context.MhkProducts, "MhkProductId", "MhkName", orderDetail.MhkProductId);
            return View(orderDetail);
        }

        // GET: MhkOrderDetails/MhkEdit/5
        public async Task<IActionResult> MhkEdit(int? id)
        {
            if (id == null) return NotFound();

            var orderDetail = await _context.MhkOrderDetails.FindAsync(id);
            if (orderDetail == null) return NotFound();

            ViewData["MhkOrderId"] = new SelectList(_context.MhkOrders, "MhkOrderId", "MhkOrderId", orderDetail.MhkOrderId);
            ViewData["MhkProductId"] = new SelectList(_context.MhkProducts, "MhkProductId", "MhkName", orderDetail.MhkProductId);
            return View(orderDetail);
        }

        // POST: MhkOrderDetails/MhkEdit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkEdit(int id, [Bind("MhkOrderDetailId,MhkOrderId,MhkProductId,MhkQuantity,MhkUnitPrice")] MhkOrderDetail orderDetail)
        {
            if (id != orderDetail.MhkOrderDetailId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MhkOrderDetailExists(orderDetail.MhkOrderDetailId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(MhkIndex));
            }
            ViewData["MhkOrderId"] = new SelectList(_context.MhkOrders, "MhkOrderId", "MhkOrderId", orderDetail.MhkOrderId);
            ViewData["MhkProductId"] = new SelectList(_context.MhkProducts, "MhkProductId", "MhkName", orderDetail.MhkProductId);
            return View(orderDetail);
        }

        // GET: MhkOrderDetails/MhkDelete/5
        public async Task<IActionResult> MhkDelete(int? id)
        {
            if (id == null) return NotFound();

            var orderDetail = await _context.MhkOrderDetails
                .Include(o => o.MhkOrder)
                .Include(o => o.MhkProduct)
                .FirstOrDefaultAsync(m => m.MhkOrderDetailId == id);

            if (orderDetail == null) return NotFound();

            return View(orderDetail);
        }

        // POST: MhkOrderDetails/MhkDelete/5
        [HttpPost, ActionName("MhkDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MhkDeleteConfirmed(int id)
        {
            var orderDetail = await _context.MhkOrderDetails.FindAsync(id);
            if (orderDetail != null)
            {
                _context.MhkOrderDetails.Remove(orderDetail);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MhkIndex));
        }

        private bool MhkOrderDetailExists(int id)
        {
            return _context.MhkOrderDetails.Any(e => e.MhkOrderDetailId == id);
        }
    }
}
