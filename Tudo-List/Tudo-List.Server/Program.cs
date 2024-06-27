using Microsoft.EntityFrameworkCore;
using Tudo_list.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    const string connection = "SqlServer";
    const string envServer = "DB_SERVER";
    const string envDatabase = "DB_DATABASE";

    var connectionString = builder.Configuration.GetConnectionString(connection)
        .Replace($"{{{envServer}}}", Environment.GetEnvironmentVariable(envServer))
        .Replace($"{{{envDatabase}}}", Environment.GetEnvironmentVariable(envDatabase));

    options.UseSqlServer(connectionString);
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();