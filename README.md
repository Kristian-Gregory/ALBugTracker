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
## Prerequisites
You'll need docker, including docker-compose

## execution
To run the application, from the 'BugTracker' folder run

docker-compose up

# Testing the application
the docker-compose command above will start a 4th container that will execute a set of integration tests that target the API. It doesn't do anything with the test results at present but the output can be examined by inspecting the container logs.

I've added a static port of 8443 on the front end web application so you should always be able to find the web application at
https://localhost:8443

# Swagger API documentation
Swagger docs for the API can be found at
http://localhost:8180/swagger

# Improvements and Next Steps
Currently the API is using http, this needs to be made https 
* End-to-end browser testing would be useful here, a selenium based test repository would add an important layer
* GitHub actions to create a CI pipeline for code scanning would help raise overall code quality
* The application needs authentication
* Razor unit tests could be introduced. I haven't introduced them already as the razor logic is mostly boiler plate, and also in the interests of time


# Difficulties encountered
I found I needed to switch to a more powerful development machine as the containers, particularly the sequel server container, were quite heavyweight and I initially set out on an inadequate machine for a dev task involving locally hosting this many containers.
Documentation was more difficult to come by for developing the app after I separated the API and the WebApp into separate containers
EF6.0 allowed default lazy loading, whereas EFCore requires child objects to be explicitly requested for loading. This took some time to identify and debug.
Visual Studio doesn't currently support hot loading for web apps hosted in containers, slowing the dev/test cycle considerably
EFCore relies heavily on convention, and if you stray from those conventions, it can be really tough to work out what's going on
