using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spice.Data;
using Spice.Models;

namespace Spice.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET Admin/Category/Index
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();

            return View(categories);
        }

        // GET Admin/Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST Admin/Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category model)
        {
            if (!ModelState.IsValid) return View(model);

            await _context.Categories.AddAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}