using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BugTrackerAPI.BugDb;
using BugTrackerModel;

namespace BugTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugsController : ControllerBase
    {
        private readonly BugDbContext _context;

        public BugsController(BugDbContext context)
        {
            _context = context;
        }

        // GET: api/Bugs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bug>>> GetBugs()
        {
            var bugs = await _context.Bugs.Include("Person").ToListAsync();
            return bugs;
        }

        // GET: api/Bugs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bug>> GetBug(int id)
        {
            var bug = await _context.Bugs.Include("Person").FirstOrDefaultAsync(bug => bug.BugId == id);

            if (bug == null)
            {
                return NotFound();
            }

            return bug;
        }

        // PUT: api/Bugs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBug(int id, Bug bug)
        {
            if (id != bug.BugId)
            {
                return BadRequest();
            }

            _context.Entry(bug).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BugExists(id))
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

        // POST: api/Bugs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bug>> PostBug(Bug bug)
        {
            _context.Bugs.Add(bug);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBug", new { id = bug.BugId }, bug);
        }

        // DELETE: api/Bugs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBug(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }

            _context.Bugs.Remove(bug);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BugExists(int id)
        {
            return _context.Bugs.Any(e => e.BugId == id);
        }
    }
}
