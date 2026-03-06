using GymManagementSystem;
using GymManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gym Management System API v1");
    options.RoutePrefix = "swagger"; 
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
