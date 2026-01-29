var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.MapGet("", () => Results.Ok("Modular Shop API"));

app.Run();
