using Microsoft.EntityFrameworkCore.Sqlite;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using BookingPetProject.Data;
using BookingPetProject.Endpoints;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite("Data Source=baseDB.db"));
builder.Services.AddAuthentication("Cookies").AddCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
});
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", () => Results.File($"{Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "login.html")}", "text/html"));

app.MapAuthEndpoints();
app.MapAdminEndpoints();
app.MapUserEnpoints();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();