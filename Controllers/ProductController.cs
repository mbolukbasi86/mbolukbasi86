using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BootStrapECommerce.Data;
using BootStrapECommerce.Models;

namespace BootStrapECommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Product or /Product/Index
        public async Task<IActionResult> Index(string? category, string? search)
        {
            var query = _context.Products.Where(p => p.IsActive);

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
                ViewBag.SelectedCategory = category;
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.Contains(search) || (p.Description != null && p.Description.Contains(search)));
                ViewBag.Search = search;
            }

            var products = await query.OrderByDescending(p => p.CreatedAt).ToListAsync();
            var categories = await _context.Products.Where(p => p.IsActive && p.Category != null)
                .Select(p => p.Category!).Distinct().OrderBy(c => c).ToListAsync();

            ViewBag.Categories = categories;
            return View(products);
        }

        // GET: /Product/Detail/5
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _context.Products
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
            if (product == null)
            {
                return NotFound();
            }

            var relatedProducts = await _context.Products
                .Where(p => p.Category == product.Category && p.Id != product.Id && p.IsActive)
                .Take(4)
                .ToListAsync();

            ViewBag.RelatedProducts = relatedProducts;
            return View(product);
        }
    }
}
