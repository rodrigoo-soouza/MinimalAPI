using Microsoft.EntityFrameworkCore;
using PersonCRUD.Data;
using PersonCRUD.Model;

namespace PersonCRUD.Route;

public static class PersonRoute
{
    public static void PersonRoutes(this WebApplication app)
    {
        var route = app.MapGroup("person");

        route.MapPost("", 
            async (PersonRequest req, AppDbContext context) =>
            {
                var person = new Person(req.name);
                await context.AddAsync(person);
                await context.SaveChangesAsync();
            });

        route.MapGet("", async (AppDbContext context) =>
        {
            var people = await context.Peoples.ToListAsync();
            return Results.Ok(people);
        });

        route.MapPut("{id:guid}",
            async (Guid id, PersonRequest req, AppDbContext context) =>
            {
                var person = await context.Peoples
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (person == null)
                {
                    return Results.NotFound();
                }

                person.ChangeName(req.name);
                await context.SaveChangesAsync();
                
                return Results.Ok(person);
            });

        route.MapDelete("{id:guid}",
            async (Guid id, AppDbContext context) =>
        {
            var person = await context.Peoples
                .FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            
                return Results.NotFound();
            person.SetInactive();
            await context.SaveChangesAsync();
            return Results.Ok(person);
        });
    } 
    
}
