var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "SmartCity API");

app.Run();
