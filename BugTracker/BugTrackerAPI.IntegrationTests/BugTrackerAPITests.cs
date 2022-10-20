
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

namespace BugTrackerAPI.IntegrationTests
{
    public class BugTrackerAPITests : TestingCaseFixture<TestingStartup>
    {
        [Fact(DisplayName = "Get bug list")]
        public async Task GetBug_All_ShouldReturnAllBugs()
        {
            // Arrange
            Console.WriteLine($"base address is '{Client.BaseAddress}'");
            var request = new System.Net.Http.HttpRequestMessage();
            // bugtrackerapi is the container name
            request.RequestUri = new Uri("http://bugtrackerapi/api/Bugs");
            request.Method = HttpMethod.Get;

            // Act
            var response = await Client.SendAsync(request);


            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Get bug 1")]
        public async Task GetBug_first_RespondsOk()
        {
            // Arrange

            var request = new System.Net.Http.HttpRequestMessage();
            // bugtrackerapi is the container name
            request.RequestUri = new Uri("http://bugtrackerapi/api/Bugs/5");
            request.Method = HttpMethod.Get;
            // Act
            var response = await Client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
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

            // Call *bugtrackerapi*, and display its response in the page
            var request = new System.Net.Http.HttpRequestMessage();

            request.RequestUri = new Uri("http://bugtrackerapi/api/Bugs");
            request.Method = HttpMethod.Post;

            var json = JsonConvert.SerializeObject(bug);

            request.Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await Client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
