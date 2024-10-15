using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Matcher.DATA.Models;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using Matcher.API.Models;

namespace WebApplication122.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchOptionsController : ControllerBase
    {
        private readonly MatcherDBContext _context;

        public MatchOptionsController(MatcherDBContext context)
        {
            _context = context;
        }

        // GET: api/MatchOptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchOption>>> GetMatchOptions()
        {
            var list = await _context.MatchOptions.Include(x => x.Options).ToListAsync();
            return list == null
                ? (ActionResult<IEnumerable<MatchOption>>)NotFound()
                : (ActionResult<IEnumerable<MatchOption>>)Ok(new Response<List<MatchOption>>
                {
                    Data = list,
                    IsSuccess = true,
                });
        }

        // GET: api/MatchOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MatchOption>> GetMatchOption(int id)
        {
            var matchOption = await _context.MatchOptions.FindAsync(id);

            if (matchOption == null)
            {
                return NotFound();
            }

            return matchOption;
        }

        // PUT: api/MatchOptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatchOption(int id, MatchOption matchOption)
        {
            if (id != matchOption.Id)
            {
                return BadRequest();
            }

            _context.Entry(matchOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchOptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MatchOptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MatchOption>> PostMatchOption(MatchOption matchOption)
        {
            _context.MatchOptions.Add(matchOption);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatchOption", new { id = matchOption.Id }, matchOption);
        }

        // DELETE: api/MatchOptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchOption(int id)
        {
            var matchOption = await _context.MatchOptions.FindAsync(id);
            if (matchOption == null)
            {
                return NotFound();
            }

            _context.MatchOptions.Remove(matchOption);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchOptionExists(int id)
        {
            return _context.MatchOptions.Any(e => e.Id == id);
        }
    }
}
