using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tudo_list.Infrastructure.Configuration.Constants;
using Tudo_list.Infrastructure.CrossCutting.Ioc;
using Tudo_List.Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("secrets/secrets.json", optional: true, reloadOnChange: true);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration[SecretsKeys.JwtPrivateKey]!)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
        };
    });

builder.Services.AddApplicationSecrets();
builder.Services.AddControllers();
builder.Services.AddAutoMapper();
builder.Services.AddDatabaseContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddDomainServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddMvc();
builder.Services.ConfigureProblemDetailsModelState();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = ApiVersion.Default;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

var app = builder.Build();

app.EnsureDatabaseCreated();
app.UseAuthentication();
app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseProblemDetailsExceptionHandler();
app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();