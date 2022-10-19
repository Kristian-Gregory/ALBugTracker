# Overview

This is a simple Bug Tracker web application. 

# Architecture and Tech stack
The application is divided into three containers, the web application ("front-end"), the API ("back-end") and a database container is provided. 

The front-end is an ASP.NET web application using Razor pages. The back-end is an ASP.NET API. The back and front end share a data model library. The back-end exposes an API, and the API has Swagger documentation to assist in developing with this API. The back-end uses an EFCore code-first migrations approach to create the database and seed it with test data. The database itself is a SQL Server 2019 on Linux container. 

When I set out to design the application I wanted to optimize for the following.

* I wanted your experience of building and running the application to be as painless as possible, with a fast journey to get up and running and without having to heavily modify your own development environment in order to be able to execute
* I wanted a consistent experience, I wanted to guarantee that how the application looked and behaved in my development environment exactly as it would when you execute it on yours
* I wanted a design that felt cloud-native, such that it was clear from the outset how this might scale in terms of adding additional microservices, or adding more container instances under load scenarios
* I wanted the database to be very loosely coupled, so that it is easy to see how you might go from developing with a local DB container, to a production database service
* I wanted the database to be drawn from code automatically so I didn't need to add any additional steps for you to get started, and to allow for the database to be casually disposed of and rebuilt during early development

# Running the application
To run the application, from the root folder execute
docker-compose -f BugTracker\docker-compose.yml -f "BugTracker\docker-compose.override.yml" -p dockercompose --ansi never up -d bugdb bugtrackerapi bugtrackerfrontend

go to URL

# Swagger API documentation
TODO: provide a static URL to the swagger docs on the API

# Difficulties encountered
I found I needed to switch to a more powerful development machine as the containers, particularly the sequel server container, were quite heavyweight and I initially set out on an inadequate machine for a dev task involving locally hosting this many containers.
Documentation was more difficult to come by for developing the app after I separated the API and the WebApp into separate containers
EF6.0 allowed default lazy loading, whereas EFCore requires child objects to be explicitly requested for loading. This took some time to identify and debug.
