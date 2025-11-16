using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using BookingPetProject.Data;
using BookingPetProject.Models; 
namespace BookingPetProject.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/auth");

            group.MapPost("/login", async (HttpContext context, ApplicationContext db) =>
            {
                var form = await context.Request.ReadFormAsync();
                var email = form["email"].ToString();
                var password = form["password"].ToString();
                var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
                if (user is null) return Results.BadRequest("Invalid credentials");
                var claims = new List<Claim> {
                    new(ClaimTypes.NameIdentifier, user.Id!),
                    new(ClaimTypes.Name, user.Name!),
                    new(ClaimTypes.Role, user.Role!)
                    };

                var identity = new ClaimsIdentity(claims, "Cookies");

                await context.SignInAsync(new ClaimsPrincipal(identity));
                return Results.Ok("Logged in");
            });

            group.MapPost("/logout", async (HttpContext context) =>
            {
                await context.SignOutAsync();
                return Results.Ok("Logged out");
            });
        }
    }
                       
}