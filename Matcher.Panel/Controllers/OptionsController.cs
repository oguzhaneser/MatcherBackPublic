using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matcher.DATA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Matcher.Panel.Controllers
{
    public class OptionsController : Controller
    {
        private readonly MatcherDBContext _context;

        public OptionsController(MatcherDBContext context)
        {
            _context = context;
        }

        // GET: Options
        public async Task<IActionResult> Index()
        {
            var matcherDBContext = _context.Options.Include(o => o.MatchOption);
            return View(await matcherDBContext.ToListAsync());
        }

        // GET: Options/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Options == null)
            {
                return NotFound();
            }

            var option = await _context.Options
                .Include(o => o.MatchOption)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (option == null)
            {
                return NotFound();
            }

            return View(option);
        }

        // GET: Options/Create
        public IActionResult Create()
        {
            ViewData["MatchOptionId"] = new SelectList(_context.MatchOptions, "Id", "Id");
            return View();
        }

        // POST: Options/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedDate,ModifiedDate,IsDeleted,IsActive,MatchOptionId,Name")] Option option)
        {
            if (ModelState.IsValid)
            {
                _context.Add(option);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MatchOptionId"] = new SelectList(_context.MatchOptions, "Id", "Id", option.MatchOptionId);
            return View(option);
        }

        // GET: Options/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Options == null)
            {
                return NotFound();
            }

            var option = await _context.Options.FindAsync(id);
            if (option == null)
            {
                return NotFound();
            }
            ViewData["MatchOptionId"] = new SelectList(_context.MatchOptions, "Id", "Id", option.MatchOptionId);
            return View(option);
        }

        // POST: Options/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedDate,ModifiedDate,IsDeleted,IsActive,MatchOptionId,Name")] Option option)
        {
            if (id != option.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(option);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OptionExists(option.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MatchOptionId"] = new SelectList(_context.MatchOptions, "Id", "Id", option.MatchOptionId);
            return View(option);
        }

        // GET: Options/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Options == null)
            {
                return NotFound();
            }

            var option = await _context.Options
                .Include(o => o.MatchOption)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (option == null)
            {
                return NotFound();
            }

            return View(option);
        }

        // POST: Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Options == null)
            {
                return Problem("Entity set 'MatcherDBContext.Options'  is null.");
            }
            var option = await _context.Options.FindAsync(id);
            if (option != null)
            {
                _context.Options.Remove(option);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OptionExists(int id)
        {
            return _context.Options.Any(e => e.Id == id);
        }
    }
}
