using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Project.Core.Cors;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                                .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
var CorsName = "Eclinic";
builder.Services.AddMyCors(CorsName);
var app = builder.Build();
app.UseCors(CorsName);
await app.UseOcelot();
app.UseWebSockets();
app.Run();
