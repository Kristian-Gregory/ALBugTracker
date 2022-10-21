using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BugTrackerFrontend.Data;
using BugTrackerModel;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BugTrackerFrontend.Pages.Bugs
{
    public class DetailsModel : PageModel
    {
      public Bug Bug { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage();

                request.RequestUri = new Uri($"http://bugtrackerapi/api/Bugs/{id}");
                request.Method = HttpMethod.Get;

                var response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                var json = await response.Content.ReadAsStringAsync();

                Bug = JObject.Parse(json).ToObject<Bug>();
            }

            return Page();

        }
    }
}
