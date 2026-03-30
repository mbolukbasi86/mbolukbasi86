using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerce.Data;
using ECommerce.Models;

namespace ECommerce.Controllers.Admin
{
    [Route("admin/kategoriler")]
    public class CategoriesAdminController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesAdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: admin/kategoriler
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .OrderBy(c => c.Name)
                .ToListAsync();
            return View("~/Views/Admin/Categories/Index.cshtml", categories);
        }

        // GET: admin/kategoriler/ekle
        [HttpGet("ekle")]
        public IActionResult Create()
        {
            return View("~/Views/Admin/Categories/Create.cshtml");
        }

        // POST: admin/kategoriler/ekle
        [HttpPost("ekle")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,IsActive")] Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedAt = DateTime.UtcNow;
                _context.Add(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"'{category.Name}' kategorisi başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Admin/Categories/Create.cshtml", category);
        }

        // GET: admin/kategoriler/duzenle/5
        [HttpGet("duzenle/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            return View("~/Views/Admin/Categories/Edit.cshtml", category);
        }

        // POST: admin/kategoriler/duzenle/5
        [HttpPost("duzenle/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,IsActive,CreatedAt")] Category category)
        {
            if (id != category.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = $"'{category.Name}' kategorisi başarıyla güncellendi.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Admin/Categories/Edit.cshtml", category);
        }

        // POST: admin/kategoriler/sil/5
        [HttpPost("sil/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();

            if (category.Products.Any())
            {
                TempData["Error"] = $"'{category.Name}' kategorisi silinemez. Bu kategoriye ait {category.Products.Count} ürün bulunmaktadır.";
                return RedirectToAction(nameof(Index));
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            TempData["Success"] = $"'{category.Name}' kategorisi başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
