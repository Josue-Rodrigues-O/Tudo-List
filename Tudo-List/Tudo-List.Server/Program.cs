using Tudo_list.Infrastructure.CrossCutting.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper();
builder.Services.AddDatabaseContext(builder.Configuration.GetConnectionString("SqlServer"));
builder.Services.AddRepositories();
builder.Services.AddDomainServices();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();