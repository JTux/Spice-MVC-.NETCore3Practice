﻿using System;
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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();

            return View(categories);
        }

        // GET Admin/Category/Create
        [HttpGet]
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


        // GET Admin/Category/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();

            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }


        // GET Admin/Category/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST Admin/Category/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category model)
        {
            if (!ModelState.IsValid) return View(model);

            var categoryFromDb = await _context.Categories.FindAsync(model.CategoryId);
            if (categoryFromDb == null) return BadRequest();

            categoryFromDb.Name = model.Name;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET Admin/Category/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST Admin/Category/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var categoryFromDb = await _context.Categories.FindAsync(id);
            if (categoryFromDb == null) return BadRequest();

            _context.Categories.Remove(categoryFromDb);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}