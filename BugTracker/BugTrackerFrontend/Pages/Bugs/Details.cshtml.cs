using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BugTrackerFrontend.Data;
using BugTrackerModel;

namespace BugTrackerFrontend.Pages.Bugs
{
    public class DetailsModel : PageModel
    {
        private readonly BugTrackerFrontend.Data.BugTrackerFrontendContext _context;

        public DetailsModel(BugTrackerFrontend.Data.BugTrackerFrontendContext context)
        {
            _context = context;
        }

      public Bug Bug { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Bug == null)
            {
                return NotFound();
            }

            var bug = await _context.Bug.FirstOrDefaultAsync(m => m.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }
            else 
            {
                Bug = bug;
            }
            return Page();
        }
    }
}
