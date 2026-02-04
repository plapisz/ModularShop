using ModularShop.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddModuleConfiguration();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
app.UseInfrastructure();
app.MapControllers();
app.MapGet("", () => Results.Ok("Modular Shop API"));

app.Run();
