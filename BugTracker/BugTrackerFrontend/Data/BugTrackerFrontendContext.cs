using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BugTrackerModel;

namespace BugTrackerFrontend.Data
{
    public class BugTrackerFrontendContext : DbContext
    {
        public BugTrackerFrontendContext (DbContextOptions<BugTrackerFrontendContext> options)
            : base(options)
        {
        }

        public DbSet<BugTrackerModel.Bug> Bug { get; set; } = default!;
    }
}
