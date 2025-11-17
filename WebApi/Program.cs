using Application;
using Infrastructure;
using Infrastructure.Persistences;
using Infrastructure.Persistences.Seeds;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerConfigurations();
builder.Services.AddAuthConfigurations(builder.Configuration);
builder.Services.AddConnectionConfigurations(builder.Configuration);
builder.Services.AddRepositories(builder.Configuration);
builder.Services.AddApplicationServices();
var app = builder.Build();

try
{
    await using var scope = app.Services.CreateAsyncScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await DefaultUsers.SeedAsync(context);
}
catch (Exception e)
{
    Console.WriteLine($"An error occurred while seeding the db: {e.Message}");
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

