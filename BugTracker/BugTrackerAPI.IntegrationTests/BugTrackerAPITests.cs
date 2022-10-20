
using Xunit;
using System;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BugTrackerAPI.IntegrationTests.Setup;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using BugTrackerModel;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace BugTrackerAPI.IntegrationTests
{
    public class BugTrackerAPITests : TestingCaseFixture<TestingStartup>
    {
        [Fact(DisplayName = "Get bug list")]
        public async Task GetBug_All_ShouldReturnAllBugs()
        {
            // Arrange

            // Act
            var response = await Client.GetAsync("/api/Bugs");

            // Assert
            var bugJson = await response.Content.ReadAsStringAsync();
            var jbugs = JArray.Parse(bugJson);
            var bugs = jbugs.ToObject<List<Bug>>();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            bugs.First<Bug>().Title.Should().Be("Lag spike on register journey on Linux client");
            
        }

        [Fact(DisplayName = "Get bug 5")]
        public async Task GetBug_five_ContainsAssignee()
        {
            // Arrange

            // Act
            var response = await Client.GetAsync("api/Bugs/5");

            // Assert
            var bugJson = await response.Content.ReadAsStringAsync();
            var bug = JObject.Parse(bugJson).ToObject<Bug>();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            bug.Assignee.Name.Should().Be("Grace Hopper");
        }

        [Fact(DisplayName = "Create new bug")]
        public async Task CreateBug_random_RespondsOk()
        {
            // Arrange
            Bug bug = new Bug
            {
                Title = "test bug",
                Description = "a very serious testing bug",
                ReportedDate = DateTime.Now,
                State = BugState.Open
            };

            // Act
            var response = await Client.PostAsJsonAsync("/api/Bugs", bug);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
