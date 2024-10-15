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
    public class MatchOptionsController : Controller
    {
        private readonly MatcherDBContext _context;

        public MatchOptionsController(MatcherDBContext context)
        {
            _context = context;
        }

        // GET: MatchOptions
        public async Task<IActionResult> Index()
        {
              return View(await _context.MatchOptions.ToListAsync());
        }

        // GET: MatchOptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MatchOptions == null)
            {
                return NotFound();
            }

            var matchOption = await _context.MatchOptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchOption == null)
            {
                return NotFound();
            }

            return View(matchOption);
        }

        // GET: MatchOptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MatchOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatedDate,ModifiedDate,IsDeleted,IsActive,Name")] MatchOption matchOption)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matchOption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(matchOption);
        }

        // GET: MatchOptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MatchOptions == null)
            {
                return NotFound();
            }

            var matchOption = await _context.MatchOptions.FindAsync(id);
            if (matchOption == null)
            {
                return NotFound();
            }
            return View(matchOption);
        }

        // POST: MatchOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatedDate,ModifiedDate,IsDeleted,IsActive,Name")] MatchOption matchOption)
        {
            if (id != matchOption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchOption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchOptionExists(matchOption.Id))
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
            return View(matchOption);
        }

        // GET: MatchOptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MatchOptions == null)
            {
                return NotFound();
            }

            var matchOption = await _context.MatchOptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchOption == null)
            {
                return NotFound();
            }

            return View(matchOption);
        }

        // POST: MatchOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MatchOptions == null)
            {
                return Problem("Entity set 'MatcherDBContext.MatchOptions'  is null.");
            }
            var matchOption = await _context.MatchOptions.FindAsync(id);
            if (matchOption != null)
            {
                _context.MatchOptions.Remove(matchOption);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchOptionExists(int id)
        {
          return _context.MatchOptions.Any(e => e.Id == id);
        }
    }
}
