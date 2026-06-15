using BloggingPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloggingPlatform.Infrastructure.Pertistence
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext context) 
        {
            await context.Database.MigrateAsync();

            if (await context.Users.AnyAsync()) return;

            var defaultAuthor = new User("ernesto_dev", "ernesto_vivero@outlook.com");

            await context.Users.AddAsync(defaultAuthor);

            await context.SaveChangesAsync();

            // Imprimimos el ID en la consola de depuración para que lo puedas copiar fácilmente al probar
            Console.WriteLine($"==================================================");
            Console.WriteLine($"USUARIO SEMILLA CREADO:");
            Console.WriteLine($"Username: {defaultAuthor.Username}");
            Console.WriteLine($"AuthorId (GUID): {defaultAuthor.Id}");
            Console.WriteLine($"==================================================");
        }
    }
}
