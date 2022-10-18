using BugTrackerAPI.BugDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace BugTrackerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CounterController : ControllerBase
    {
        private readonly ILogger<CounterController> _logger;
        private readonly IDistributedCache _cache;
        private readonly BugDbContext _bugDbContext;

        public CounterController(ILogger<CounterController> logger, IDistributedCache cache, BugDbContext bugDbContext)
        {
            _logger = logger;
            _cache = cache;
            _bugDbContext = bugDbContext ?? throw new ArgumentNullException(nameof(bugDbContext));
        }

        [HttpGet(Name = "GetCounter")]
        public async Task<string> Get()
        {
            string? result = null;
            try
            {
                var numBugs = await _bugDbContext.Bugs.CountAsync();
                result = numBugs.ToString();
            }
            catch (Exception ex)
            {
                result = $"Failed to count all the bugs! " + ex.ToString();
            }
            return result;
        }
    }
}