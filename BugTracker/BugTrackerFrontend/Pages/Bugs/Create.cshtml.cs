using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BugTrackerFrontend.Data;
using BugTrackerModel;
using Newtonsoft.Json.Linq;
using System.Security.Policy;
using System.Text;
using Newtonsoft.Json;
using System.Net.Mime;

namespace BugTrackerFrontend.Pages.Bugs
{
    public class CreateModel : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Bug Bug { get; set; }
        
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage();

                request.RequestUri = new Uri("http://bugtrackerapi/api/Bugs");
                request.Method = HttpMethod.Post;

                var json = JsonConvert.SerializeObject(Bug);

                request.Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
                var response = await client.SendAsync(request);
            }

            // TODO: lets redirect to the details page of the created bug

            return RedirectToPage("./../Index");
        }
    }
}
