# HotelListing.API

This is an ASP.NET WebAPI project that provides a backend for a hotel listing application. It uses .NET Core 3.1 and Entity Framework Core to provide CRUD operations for hotel listings.

## Project Structure
The project is divided into three main components:

#### HotelListing.API: 
This contains the WebAPI controllers and the Program.cs file that configures the WebAPI.
This is the main project that ties together the WebAPI and data access layers. It references the HotelListing.API.Core and HotelListing.API.Data projects.

#### HotelListing.API.Data: 
This contains the Entity Framework Core data access layer, and Models, including the AppDbContext class that defines the database schema.

#### HotelListing.API.Core: 
This contains the WebAPI data transfer objects (DTOs), IRepository interface that provides a common interface for CRUD operations, and the other Interfaces that will be used in the WebAPI.

## Project Updates
#### Refactor The Repository: 
This update made changes to the IRepository interface in the HotelListing.API.Core project, likely to improve the consistency of the interface or update the implementation details of the data access layer.

#### Add JWT Authentication to Swagger Doc: 
This update likely added JWT authentication support to the Swagger documentation, which would enable testing authenticated API endpoints using Swagger UI.

#### Project Architectural Changes: 
This update made significant changes to the overall architecture of the project, introducing new projects or reorganizing existing code.
