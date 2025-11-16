using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BookingPetProject.Data;
using BookingPetProject.Models;
using BookingPetProject.Models.DTOs;

namespace BookingPetProject.Endpoints
{
    public static class AdminEndpoints
    {
        public static void MapAdminEndpoints(this IEndpointRouteBuilder app)
        {
            var admin = app.MapGroup("/admin").RequireAuthorization(new AuthorizeAttribute { Roles = "admin" });

            admin.MapGet("/master-class", async (ApplicationContext db) =>
            {
                var masterClasses = await db.MasterClasses.Select(mc => new MasterClassDto
                {
                    Id = mc.Id,
                    Name = mc.Name!,
                    Description = mc.Description!,
                    DateTimeEvent = mc.DateTimeEvent,
                    Capacity = mc.Capacity
                }).ToListAsync();

                return Results.Ok(masterClasses);
            });

            admin.MapPost("/master-class/create", async (ApplicationContext db, CreateMasterClassDto dto) =>
            {
 
                var mc = new MasterClass
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    DateTimeEvent = dto.DateTimeEvent,
                    Capacity = dto.Capacity
                };

                db.MasterClasses.Add(mc);
                await db.SaveChangesAsync();

                return Results.Created($"/admin/master-class/{mc.Id}", mc);
            });

            admin.MapGet("/master-class/{id:int}", async (int id, ApplicationContext db) =>
            {
                var mc = await db.MasterClasses.FindAsync(id);

                if (mc is null) return Results.NotFound();

                return Results.Ok(new MasterClassDto
                {
                    Id = mc.Id,
                    Name = mc.Name!,
                    Description = mc.Description!,
                    DateTimeEvent = mc.DateTimeEvent,
                    Capacity = mc.Capacity
                });
            });

            admin.MapPut("/master-class/{id:int}/edit", async (int id, CreateMasterClassDto dto, ApplicationContext db) =>
            {
                var mc = await db.MasterClasses.FindAsync(id);
                if (mc is null) return Results.NotFound();

                mc.Name = dto.Name;
                mc.Description = dto.Description;
                mc.DateTimeEvent = dto.DateTimeEvent;
                mc.Capacity = dto.Capacity;

                await db.SaveChangesAsync();
                return Results.Ok(mc);
            });

            admin.MapDelete("/master-class/{id:int}/delete", async (int id, ApplicationContext db) =>
            {
                var mc = await db.MasterClasses.FindAsync(id);
                if (mc is null) return Results.NotFound();

                db.MasterClasses.Remove(mc);
                await db.SaveChangesAsync();
                return Results.Ok();
            });

            admin.MapGet("/master-class/{id:int}/participants", async (int id, ApplicationContext db) =>
            {
                var users = await db.Bookings.Where(b => b.MasterClassId == id && b.BookingStatus != Booking.Status.Cancelled).Select(b => b.User!).Select(u => new UserDto
                {
                    Id = u.Id!,
                    Name = u.Name!,
                    Email = u.Email!,
                    Role = u.Role!
                }).ToListAsync();

                return Results.Ok(users);
            });
        }
    }
}