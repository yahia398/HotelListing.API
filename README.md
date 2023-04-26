# HotelListing.API

This is an ASP.NET WebAPI project that provides a backend for a hotel listing application. It uses .NET Core 7 and Entity Framework Core to provide CRUD operations for hotel listings.

## Project Structure
The project is divided into three main components:

### HotelListing.API: 
This contains the WebAPI controllers and the Program.cs file that configures the WebAPI.
This is the main project that ties together the WebAPI and data access layers. It references the HotelListing.API.Core and HotelListing.API.Data projects.

### HotelListing.API.Data: 
This contains the Entity Framework Core data access layer, and Models, including the AppDbContext class that defines the database schema.

### HotelListing.API.Core: 
This contains the WebAPI data transfer objects (DTOs), IRepository interface that provides a common interface for CRUD operations, and the other Interfaces that will be used in the WebAPI.

## Added Value Features
The following features have been added to the HotelListing API to improve its functionality and performance:
* Asynchronous Programming:

The HotelListing API uses asynchronous programming techniques to improve its scalability and responsiveness. This allows the API to handle multiple requests simultaneously without blocking threads, which can lead to better performance and faster response times.

* Custom Logging using SeriLog:

The API uses SeriLog, a structured logging library for .NET, to provide custom logging capabilities. This enables developers to easily configure and customize logging to meet their specific needs, including logging to different destinations like the console or a file.

* Use Swagger UI Documentation:

The API is documented using Swagger UI, which provides a user-friendly interface for exploring and testing the API endpoints. This documentation can also be used to generate client-side code for calling the API, which can save time and reduce errors when integrating with the API.

* API Versioning:

The API supports versioning, which allows developers to make changes to the API without breaking existing clients. This is especially useful when developing APIs that are consumed by multiple clients, as it allows clients to continue using a specific version of the API even if newer versions are available.

* Configure Cross-Origin Resource Sharing (CORS):

The API is configured to support Cross-Origin Resource Sharing (CORS), which allows web applications to make requests to the API from different domains. This is especially useful when developing single-page applications or integrating with third-party services that are hosted on different domains.

## Project Updates
* Refactor The Repository: 

This update made changes to the IRepository interface in the HotelListing.API.Core project, likely to improve the consistency of the interface or update the implementation details of the data access layer.

* Add JWT Authentication to Swagger Doc: 

This update likely added JWT authentication support to the Swagger documentation, which would enable testing authenticated API endpoints using Swagger UI.

* Project Architectural Changes: 

This update made significant changes to the overall architecture of the project, introducing new projects or reorganizing existing code.
