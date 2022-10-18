using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugTrackerFrontend.Data;
using BugTrackerModel;

namespace BugTrackerFrontend.Pages.Bugs
{
    public class EditModel : PageModel
    {
        private readonly BugTrackerFrontend.Data.BugTrackerFrontendContext _context;

        public EditModel(BugTrackerFrontend.Data.BugTrackerFrontendContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Bug Bug { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bug == null)
            {
                return NotFound();
            }

            var bug =  await _context.Bug.FirstOrDefaultAsync(m => m.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }
            Bug = bug;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Bug).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BugExists(Bug.BugId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BugExists(int id)
        {
          return _context.Bug.Any(e => e.BugId == id);
        }
    }
}
