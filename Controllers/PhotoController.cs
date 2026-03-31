using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BootStrapECommerce.Data;

namespace BootStrapECommerce.Controllers
{
    public class PhotoController : Controller
    {
        private readonly AppDbContext _context;

        public PhotoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Photo/GetImage/5
        // Serves a photo by its own ID directly from the Photos table BLOB data
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetImage(int id)
        {
            var photo = await _context.Photos.FindAsync(id);

            if (photo == null || photo.Data == null || photo.Data.Length == 0)
            {
                return Redirect("/images/products/no-image.png");
            }

            return File(photo.Data, photo.ContentType);
        }

        // GET: /Photo/GetImageByProduct/5
        // Serves the first photo of a given product — used by list views to avoid loading all Photos
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetImageByProduct(int productId)
        {
            var photo = await _context.Photos
                .Where(p => p.ProductId == productId)
                .FirstOrDefaultAsync();

            if (photo == null || photo.Data == null || photo.Data.Length == 0)
            {
                return Redirect("/images/products/no-image.png");
            }

            return File(photo.Data, photo.ContentType);
        }
    }
}
