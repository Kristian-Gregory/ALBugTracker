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
        /*
         * private readonly BugTrackerFrontend.Data.BugTrackerFrontendContext _context;

        
        public CreateModel(BugTrackerFrontend.Data.BugTrackerFrontendContext context)
        {
            _context = context;
        }
        */
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

            //_context.Bug.Add(Bug);
            //await _context.SaveChangesAsync();

            using (var client = new System.Net.Http.HttpClient())
            {
                // Call *bugtrackerapi*, and display its response in the page
                var request = new System.Net.Http.HttpRequestMessage();

                request.RequestUri = new Uri("http://bugtrackerapi/api/Bugs");
                request.Method = HttpMethod.Post;

                var json = JsonConvert.SerializeObject(Bug);

                request.Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
                var response = await client.SendAsync(request);
                /*
                var bugJson = await response.Content.ReadAsStringAsync();
                var bug = JObject.Parse(bugJson);
                string resultContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    bug.ToObject<Bug>();
                }*/

            }
            // TODO: lets redirect to the details page of the created bug

            return RedirectToPage("./../Index");
        }
    }
}
