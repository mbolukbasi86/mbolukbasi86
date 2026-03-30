using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerce.Data;
using ECommerce.Models;

namespace ECommerce.Controllers.Admin
{
    [Route("admin/urunler")]
    public class ProductsAdminController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsAdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: admin/urunler
        [HttpGet("")]
        public async Task<IActionResult> Index(string? searchTerm, int? categoryFilter, bool? activeFilter)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
                query = query.Where(p => p.Name.Contains(searchTerm) || (p.Description != null && p.Description.Contains(searchTerm)));

            if (categoryFilter.HasValue)
                query = query.Where(p => p.CategoryId == categoryFilter.Value);

            if (activeFilter.HasValue)
                query = query.Where(p => p.IsActive == activeFilter.Value);

            ViewBag.SearchTerm = searchTerm;
            ViewBag.CategoryFilter = categoryFilter;
            ViewBag.ActiveFilter = activeFilter;
            ViewBag.Categories = new SelectList(await _context.Categories.OrderBy(c => c.Name).ToListAsync(), "Id", "Name");

            var products = await query.OrderBy(p => p.Name).ToListAsync();
            return View("~/Views/Admin/Products/Index.cshtml", products);
        }

        // GET: admin/urunler/ekle
        [HttpGet("ekle")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _context.Categories.Where(c => c.IsActive).OrderBy(c => c.Name).ToListAsync(), "Id", "Name");
            return View("~/Views/Admin/Products/Create.cshtml");
        }

        // POST: admin/urunler/ekle
        [HttpPost("ekle")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,StockQuantity,ImageUrl,IsActive,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedAt = DateTime.UtcNow;
                product.UpdatedAt = DateTime.UtcNow;
                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"'{product.Name}' ürünü başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(await _context.Categories.Where(c => c.IsActive).OrderBy(c => c.Name).ToListAsync(), "Id", "Name", product.CategoryId);
            return View("~/Views/Admin/Products/Create.cshtml", product);
        }

        // GET: admin/urunler/duzenle/5
        [HttpGet("duzenle/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(await _context.Categories.OrderBy(c => c.Name).ToListAsync(), "Id", "Name", product.CategoryId);
            return View("~/Views/Admin/Products/Edit.cshtml", product);
        }

        // POST: admin/urunler/duzenle/5
        [HttpPost("duzenle/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,StockQuantity,ImageUrl,IsActive,CategoryId,CreatedAt")] Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    product.UpdatedAt = DateTime.UtcNow;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"'{product.Name}' ürünü başarıyla güncellendi.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(await _context.Categories.OrderBy(c => c.Name).ToListAsync(), "Id", "Name", product.CategoryId);
            return View("~/Views/Admin/Products/Edit.cshtml", product);
        }

        // POST: admin/urunler/sil/5
        [HttpPost("sil/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            TempData["Success"] = $"'{product.Name}' ürünü başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
