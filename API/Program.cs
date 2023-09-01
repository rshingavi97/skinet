using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using API.Middleware;
using API.Extensions;


var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAppServices(builder.Configuration);
//calling extension method
var app = builder.Build();
// Configure the HTTP request pipeline.
// HTTP request pipeline => starting point
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

//code for generating the database.
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex,"An occured during migration");
}
app.Run();
