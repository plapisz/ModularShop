using ModularShop.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInfrastructure();

var app = builder.Build();
app.UseInfrastructure();
app.MapControllers();
app.MapGet("", () => Results.Ok("Modular Shop API"));

app.Run();
