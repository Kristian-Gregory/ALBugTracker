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
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BugTrackerFrontend.Pages.Bugs
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Bug Bug { get; set; }

        [BindProperty]
        public IEnumerable<Person> AllAssignees { get; set; }

        [BindProperty]
        public string SelectedAssigneePersonId { get; set; }

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

            await PopulateAssignees(); 
            if (Bug.Person != null)
            {
                SelectedAssigneePersonId = Bug.Person.PersonId.ToString();
            }

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

            await PopulateAssignees();

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage();

                request.RequestUri = new Uri($"http://bugtrackerapi/api/Bugs/{Bug.BugId}");
                request.Method = HttpMethod.Put;

                Bug.Person = AllAssignees.First(x => x.PersonId == Int32.Parse(SelectedAssigneePersonId));
                Bug.PersonId = Int32.Parse(SelectedAssigneePersonId);

                var json = JsonConvert.SerializeObject(Bug);

                request.Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
                var response = await client.SendAsync(request);
            }

            return RedirectToPage("./../Index");
        }

        private async Task PopulateAssignees()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage();

                request.RequestUri = new Uri($"http://bugtrackerapi/api/People");
                request.Method = HttpMethod.Get;

                var response = await client.SendAsync(request);

                var json = await response.Content.ReadAsStringAsync();

                var personJson = await response.Content.ReadAsStringAsync();
                var persons = JArray.Parse(personJson);
                AllAssignees = persons.ToObject<List<Person>>();
            }
        }


    }
}
