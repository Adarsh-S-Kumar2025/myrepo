using System;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HotelBookingSystemAPI.Infrastructure.Data;
using HotelBookingSystemAPI.Application.Hotel.Commands.CreateHotel;
using HotelBookingSystemAPI.Application.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using HotelBookingSystemAPI.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Configuration: connection string fallback to the same one used by DbContext.OnConfiguring
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=localhost;Database=HotelBookingDb;User Id=sa;Password=12345678Aa;TrustServerCertificate=true;";

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core DbContext
builder.Services.AddDbContext<HotelBookingDbContext>(options =>
    options.UseSqlServer(defaultConnection));

// MediatR - explicitly register the assembly that contains your handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateHotelCommandHandler).Assembly));

builder.Services.AddHttpContextAccessor();

// JWT and TokenService
builder.Services.AddSingleton<ITokenService, TokenService>();

var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key missing in configuration");
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

var app = builder.Build();

// Apply pending EF migrations at startup (optional) and seed admin
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<HotelBookingDbContext>();
        db.Database.Migrate();

        // seed admin account if missing
        var hasher = new PasswordHasher<Employee>();
        if (!db.Employees.Any(e => e.Email == "admin@hotel.local"))
        {
            var admin = new Employee
            {
                Email = "admin@hotel.local",
                FullName = "Administrator",
                Role = "Admin"
            };
            admin.PasswordHash = hasher.HashPassword(admin, "ChangeThisPassword!");
            db.Employees.Add(admin);
            db.SaveChanges();
        }
    }
    catch
    {
        // swallow or log as appropriate for your environment
    }
}

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // must be before UseAuthorization
app.UseAuthorization();
app.MapControllers();

app.Run();