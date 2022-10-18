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
    public class IndexModel : PageModel
    {
        private readonly BugTrackerFrontend.Data.BugTrackerFrontendContext _context;

        public IndexModel(BugTrackerFrontend.Data.BugTrackerFrontendContext context)
        {
            _context = context;
        }

        public IList<Bug> Bug { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Bug != null)
            {
                Bug = await _context.Bug.ToListAsync();
            }
        }
    }
}
