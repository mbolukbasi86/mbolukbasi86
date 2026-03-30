using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerce.Data;

namespace ECommerce.Controllers.Admin
{
    [Route("admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalProducts = await _context.Products.CountAsync();
            ViewBag.ActiveProducts = await _context.Products.CountAsync(p => p.IsActive);
            ViewBag.TotalCategories = await _context.Categories.CountAsync();
            ViewBag.ActiveCategories = await _context.Categories.CountAsync(c => c.IsActive);
            ViewBag.LowStockProducts = await _context.Products.CountAsync(p => p.StockQuantity < 10);
            ViewBag.TotalStockValue = await _context.Products
                .Where(p => p.IsActive)
                .SumAsync(p => p.Price * p.StockQuantity);
            ViewBag.RecentProducts = await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedAt)
                .Take(5)
                .ToListAsync();

            return View("~/Views/Admin/Dashboard/Index.cshtml");
        }
    }
}
