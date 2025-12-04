using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Nafaa.Infrastructure.Data;
using Nafaa.Infrastructure.Models;
using Nafaa.Api.Models.Email;
using Nafaa.Api.Services;
using Nafaa.Api.Services.Email;
using System.Text.Json.Serialization;
using Nafaa.Api.Services.Auth;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Database Configuration
builder.Services.AddDbContext<NafaaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Email Settings Configuration

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));


var emailProvider = builder.Configuration["EmailSettings:Provider"] ?? "Console";

if (emailProvider == "Console")
{
    builder.Services.AddScoped<IEmailService, ConsoleEmailService>();
    Console.WriteLine("Using ConsoleEmailService for email (logs to console)");
}
else if (emailProvider == "Smtp")
{
    builder.Services.AddScoped<IEmailService, SmtpEmailService>();
    Console.WriteLine("Using SmtpEmailService (configured for SMTP)");
}
else
{
    builder.Services.AddScoped<IEmailService, ConsoleEmailService>();
    Console.WriteLine($"Unknown email provider '{emailProvider}', defaulting to ConsoleEmailService");
}

builder.Services.AddScoped<IPasswordResetService, PasswordResetService>();

// ASP.NET Identity Configuration
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<NafaaDbContext>()
.AddDefaultTokenProviders();

// JWT Auth
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });


builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// Other services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add Authentication & Authorization middleware
app.UseAuthentication(); // This must come before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();