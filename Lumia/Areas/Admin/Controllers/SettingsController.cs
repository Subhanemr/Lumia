﻿using Lumia.Areas.Admin.ViewModels;
using Lumia.DAL;
using Lumia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Lumia.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class SettingsController : Controller
    {
        private readonly AppDbContext _context;

        public SettingsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ICollection<Settings> items = await _context.Settings.ToListAsync();
            return View(items);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSettingsVM create)
        {
            if (!ModelState.IsValid) return View(create);
            bool result = await _context.Settings.AnyAsync(x => x.Key.Trim().ToLower() == x.Key.Trim().ToLower());
            if (result)
            {
                ModelState.AddModelError("Name", "Is exists");
                return View(create);
            }
            Settings item = new Settings
            {
                Key = create.Key,
                Value = create.Value
            };
            await _context.Settings.AddAsync(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Settings item = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            UpdateSettingsVM update = new UpdateSettingsVM
            {
                Key = item.Key,
                Value = item.Value
            };
            return View(update);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateSettingsVM update)
        {
            if (!ModelState.IsValid) return View(update);
            Settings item = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            bool result = await _context.Settings.AnyAsync(x => x.Key.Trim().ToLower() == x.Key.Trim().ToLower() && x.Id != id);
            if (result)
            {
                ModelState.AddModelError("Name", "Is exists");
                return View(update);
            }

            item.Key = update.Key;
            item.Value = update.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Settings item = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) return NotFound();
            _context.Settings.Remove(item);
            return RedirectToAction(nameof(Index));

        }
    }
}
