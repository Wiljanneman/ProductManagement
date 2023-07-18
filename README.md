# Product Management Dashboard

This solution demonstrates how a small SPA, in this case a product management dashboard, can be built via Clean architecture using .NET 6 as backend Api and Angular 15 as Frontend presentation layer.

## Technologies

* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [Angular](https://angular.io/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [nLog](https://nlog-project.org/)

## Demo

* [UI](https://productmanagementui.azurewebsites.net/)
* [Api](https://productmanagementservice.azurewebsites.net/api)

## Getting Started

1. Install the latest [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
2. Install the latest [Node.js LTS](https://nodejs.org/en/)
3. Navigate to `src/WebUI` and run `npm install`
4. Navigate to `src/WebUI` and run `npm serve` to launch the front end (Angular)
5. Run Api project in 'src/WebUI' via prefered method.
6. Check environment variables in src/WebUI to ensure the Angular client points to the correct backend endpoint base url
7. User Module yet to be created. __Check Program.cs for login credentials__ for an admin user and normal test user


### Database Configuration

The solution is configured to use an in-memory database (SQLite) by default. This ensures that all users will be able to run the solution without needing to set up additional infrastructure (e.g. SQL Server).

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

## Overview

Clean Architecture is a software architectural pattern that promotes the separation of concerns and emphasizes the independence of business logic from external dependencies. It provides a structured approach to designing applications with clear boundaries between different layers.

At its core, Clean Architecture consists of four main layers:

### Domain

The Domain layer encapsulates the domain entities. 

### Application

The Application layer acts as the intermediary between the Domain and Infrastructure layers. It contains application-specific logic, such as use cases, business workflows, and application services.
This layer contains all application logic, enums, exceptions, interfaces, types and logic specific to the application layer. 

### Infrastructure

The API layer is responsible for exposing the application's functionality to external clients, such as web browsers, mobile apps, or other services. It handles the incoming requests, performs necessary validations, and invokes the appropriate use cases or application services from the Application layer. The API layer also handles the serialization and deserialization of data in a format that can be easily consumed by the clients, such as JSON or XML. This layer often utilizes frameworks or libraries specific to the chosen platform or technology stack, such as ASP.NET Core, Express.js, or Flask.
### Web (Angular)

This layer is a single page application based on Angular 15.
