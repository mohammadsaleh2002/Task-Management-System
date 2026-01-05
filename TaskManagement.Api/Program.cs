using TaskManagement.Application;
using TaskManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//  DependencyInjection classes for the Application and Infrastructure projects.
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// Standard API services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure SwaggerUI
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Enable Controller mapping (Replaces manual MapGet endpoints for a clean structure)
app.UseMiddleware<TaskManagement.Api.Middleware.ExceptionHandlingMiddleware>(); 
app.MapControllers();   

app.Run();