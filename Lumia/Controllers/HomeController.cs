using Lumia.DAL;
using Lumia.Models;
using Lumia.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lumia.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<Product> products = await _context.Products.Include(x => x.Category).ToListAsync();
            ICollection<Service> services = await _context.Services.ToListAsync();

            HomeVM vM = new HomeVM { Products = products, Services = services };
            return View(vM);
        }
    }
}
