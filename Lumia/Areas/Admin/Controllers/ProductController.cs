using Lumia.Areas.Admin.ViewModels;
using Lumia.DAL;
using Lumia.Models;
using Lumia.Utilities.Extetions;
using Lumia.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lumia.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index(int page = 1)
        {
            if (page <= 0) return BadRequest();
            double count = await _context.Products.CountAsync();
            ICollection<Product> items = await _context.Products.Skip((page - 1) * 4).Take(4)
                .Include(x => x.Category).ToListAsync();

            PaginationVM<Product> vM = new PaginationVM<Product>
            {
                CurrentPage = page,
                TotalPage = Math.Ceiling(count / 4),
                Items = items
            };

            return View(vM);

        }
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create()
        {
            CreateProductVM create = new CreateProductVM
            {
                Categories = await _context.Categories.ToListAsync()
            };
            return View(create);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM create)
        {
            if (!ModelState.IsValid)
            {
                create.Categories = await _context.Categories.ToListAsync();
                return View(create);
            }
            bool result = await _context.Products.AnyAsync(x => x.Name.Trim().ToLower() == create.Name.Trim().ToLower());
            if (result)
            {
                create.Categories = await _context.Categories.ToListAsync();
                ModelState.AddModelError("Name", "Is exists");
                return View(create);
            }
            if (!create.Photo.IsValid())
            {
                create.Categories = await _context.Categories.ToListAsync();
                ModelState.AddModelError("Photo", "Is not valid");
                return View(create);
            }
            if (!create.Photo.LimitSize())
            {
                create.Categories = await _context.Categories.ToListAsync();
                ModelState.AddModelError("Photo", "Limit size 10MB");
                return View(create);
            }
            Product item = new Product
            {
                Name = create.Name,
                Price = create.Price,
                Description = create.Description,
                CategoryId = create.CategoryId,
                Img = await create.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img")
            };
            await _context.Products.AddAsync(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Product item = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            UpdateProductVM update = new UpdateProductVM
            {
                Name = item.Name,
                Categories = await _context.Categories.ToListAsync(),
                Price = item.Price,
                Description = item.Description,
                CategoryId = item.CategoryId,
                Img = item.Img

            };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateProductVM update)
        {
            if (!ModelState.IsValid)
            {
                update.Categories = await _context.Categories.ToListAsync();
                return View(update);
            }
            Product item = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            bool result = await _context.Products.AnyAsync(x => x.Name.Trim().ToLower() == update.Name.Trim().ToLower() && x.Id != id);
            if (result)
            {
                update.Categories = await _context.Categories.ToListAsync();
                ModelState.AddModelError("Name", "Is exists");
                return View(update);
            }
            if (update.Photo is not null)
            {
                if (!update.Photo.IsValid())
                {
                    update.Categories = await _context.Categories.ToListAsync();
                    ModelState.AddModelError("Photo", "Is not valid");
                    return View(update);
                }
                if (!update.Photo.LimitSize())
                {
                    update.Categories = await _context.Categories.ToListAsync();
                    ModelState.AddModelError("Photo", "Limit size 10MB");
                    return View(update);
                }
                item.Img.DeleteFileAsync(_env.WebRootPath, "assets", "img");
                item.Img = await update.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img");
            }

            item.Name = update.Name;
            item.Description = update.Description;
            item.Price = update.Price;
            item.CategoryId = update.CategoryId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Product item = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            item.Img.DeleteFileAsync(_env.WebRootPath, "assets", "img");
            _context.Products.Remove(item);
            return RedirectToAction(nameof(Index));

        }
    }
}
