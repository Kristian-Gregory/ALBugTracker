using BugTrackerModel;
using System.Globalization;

namespace BugTrackerAPI.BugDb
{
    public static class DbInitializer
    {
        public static void Initialize(BugDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.People.Any()) return;

            var ada = new Person
            {
                Name = "Ada Lovelace"
            };
            var tim = new Person
            {
                Name = "Tim Berners-Lee"
            };
            var grace = new Person
            {
                Name = "Grace Hopper"
            };
            var kev = new Person
            {
                Name = "Kevin Mitnick"
            };
            var marcus = new Person
            {
                Name = "Marcus Hutchins"
            };

            var dreamTeam = new Person[]
            {
                ada, tim, grace, kev, marcus
            };

            foreach (Person dev in dreamTeam)
            {
                context.People.Add(dev);
            }

            if (context.Bugs.Any()) return;

            CultureInfo provider = new CultureInfo("en-GB");

            var bugs = new Bug[]
            {
                new Bug
                {
                    Title = "Lag spike on register journey on Linux client",
                    Description = "Performance poor on linux client when signing up users",
                    ReportedDate = DateTime.ParseExact("2021-09-01", "yyyy-MM-dd", provider),
                    State = BugState.Open,
                    Assignee = ada
                },
                new Bug
                {
                    Title = "Unexpected application crash",
                    Description = "Application stopped working unexpectedly at exactly midnight",
                    ReportedDate = DateTime.Parse("2000-01-01"),
                    State = BugState.Closed,
                    Assignee = tim
                },
                new Bug
                {
                    Title = "Catastrophic coffee machine failure at 8:23 am",
                    Description = "Coffee machine stopped outputting valid beverage during critical business hours leading to severe developer impact",
                    ReportedDate = DateTime.Parse("2021-04-28"),
                    State = BugState.Open,
                    Assignee = kev
                },
                new Bug
                {
                    Title = "Gaps in user documentation for feature still in beta",
                    Description = "Developer insists code is self-documenting but users disagree",
                    ReportedDate = DateTime.Parse("2022-10-18"),
                    Assignee = kev
                },
                new Bug
                {
                    Title = "Lag spike on register journey on Linux client",
                    Description = "Performance poor on linux client when signing up users",
                    ReportedDate = DateTime.Parse("2002-09-01"),
                    Assignee = grace
                },
            };

            foreach (Bug bug in bugs)
            {
                context.Bugs.Add(bug);
            }

            context.SaveChanges();
        }
    }
}
