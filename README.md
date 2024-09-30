# Base Project for .NET Clean Architecture

This repository serves as a **base project template** for building scalable and maintainable applications using the **Clean Architecture** pattern in .NET. It provides a well-structured solution that follows industry best practices, aiming to accelerate development by providing a ready-to-use architecture foundation.

## Key Features:
- **Modular Architecture**: Separation of concerns into different layers (Domain, Application, Infrastructure, Presentation).
- **Generic Repository Pattern**: Easily extendable and adaptable data access layer.
- **REST API Ready**: Pre-configured support for building RESTful services.
- **CQRS Pattern**: Integrated Command and Query Responsibility Segregation for clear separation of read and write operations.
- **Dependency Injection**: Built-in support for DI, following SOLID principles.
- **Error Handling and Validation**: Centralized error handling and request validation mechanisms.
- **Unit Testing Support**: Base setup for unit testing to ensure code reliability.

## Layers:
1. **Domain**: Contains the core business logic and entities.
2. **Application**: Handles application-specific business rules, including DTOs, commands, queries, and mappers.
3. **Infrastructure**: Manages external resources like database access, third-party services, and file systems.
4. **Presentation**: Web API or MVC frontend to interact with the application.

## Getting Started:
1. Clone the repository.
2. Update the `appsettings.json` to fit your environment (database connections, external services, etc.).
3. Run the project and start building your custom features on top of this architecture.
4. Add Program.cs below code example;

 ```c#
    using CosmosBase;
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
    ...
    
            builder.Services.AddCosmosBase();
    ...
        }
    }
```
5.Use for UnitOfWork with CosmosBase;

```c#
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly IUnitOfWork<ApplicationDbContext> _unitOfWork;
        public CustomerController(IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var newCustomer = new Customer
            {
                Firstname = "John",
                Lastname = "Doe",
                Email = "johndoe@info.com",
                Phone = "1234567890",
            };
            await _unitOfWork.GetRepository<Customer>().AddAsync(newCustomer);
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }
    }
```
