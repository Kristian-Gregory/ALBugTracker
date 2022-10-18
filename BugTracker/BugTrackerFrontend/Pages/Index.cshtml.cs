using BugTrackerModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System;

namespace BugTrackerFrontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IList<Bug> _bugs;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // Call *bugtrackerapi*, and display its response in the page
                var request = new System.Net.Http.HttpRequestMessage();
                // bugtrackerapi is the container name
                request.RequestUri = new Uri("http://bugtrackerapi/api/Bugs");
                var response = await client.SendAsync(request);

                var bugJson = await response.Content.ReadAsStringAsync();
                var bugs = JArray.Parse(bugJson);
                ViewData["Message"] = $"Number of bugs in bug DB :{bugs.Count}";
                ViewData["Bugs"] = bugs.ToObject<List<Bug>>();
            }
        }
    }
}