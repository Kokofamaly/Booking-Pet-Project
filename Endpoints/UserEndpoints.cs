using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BookingPetProject.Data;
using BookingPetProject.Models;
using BookingPetProject.Models.DTOs;

namespace BookingPetProject.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEnpoints(this IEndpointRouteBuilder app)
        {
            var user = app.MapGroup("/user").RequireAuthorization(new AuthorizeAttribute { Roles = "user" });

            user.MapGet("/master-class", async (ApplicationContext db) =>
            {
                var classes = await db.MasterClasses.Select(mc => new MasterClassDto
                {
                    Id = mc.Id,
                    Name = mc.Name!,
                    Description = mc.Description!,
                    DateTimeEvent = mc.DateTimeEvent,
                    Capacity = mc.Capacity
                }).ToListAsync();

                return Results.Ok(classes);
            });

            user.MapPost("/master-class/{id:int}/sign-up", async (int id, ApplicationContext db, HttpContext context) =>
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var mc = await db.MasterClasses.FindAsync(id);
                if (mc is null) return Results.NotFound();

                int booked = await db.Bookings.CountAsync(b => b.MasterClassId == id && b.BookingStatus != Booking.Status.Cancelled);
                if (booked >= mc.Capacity)
                    return Results.BadRequest("Master class is full");

                db.Bookings.Add(new Booking
                {
                    UserId = userId,
                    MasterClassId = mc.Id,
                    BookingStatus = Booking.Status.Pending

                });

                await db.SaveChangesAsync();
                return Results.Ok();
            });

            user.MapGet("/master-class/my-list", async (HttpContext context, ApplicationContext db) =>
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

                var list = await db.Bookings
                .Include(b => b.MasterClass)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .Select(b => new BookingDto
                {
                    Id = b.Id,
                    UserId = b.UserId!,
                    UserName = b.User!.Name!,
                    MasterClassId = b.MasterClassId,
                    MasterClassName = b.MasterClass!.Name!,
                    BookingStatus = (int)b.BookingStatus
                }).ToListAsync();
                
                return Results.Json(list);
            });

            user.MapPut("/master-class/{id:int}/confirm", async (int id, HttpContext context, ApplicationContext db) =>
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var booking = await db.Bookings.FirstOrDefaultAsync(b => b.MasterClassId == id && b.UserId == userId);

                if (booking is null) return Results.NotFound();
                booking.BookingStatus = Booking.Status.Confirmed;

                await db.SaveChangesAsync();
                return Results.Ok();
            });

            user.MapPut("/master-class/{id:int}/cancel", async (int id, HttpContext context, ApplicationContext db) =>
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var booking = await db.Bookings.FirstOrDefaultAsync(b => b.MasterClassId == id && b.UserId == userId);

                if (booking is null) return Results.NotFound();
                booking.BookingStatus = Booking.Status.Cancelled;

                await db.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}