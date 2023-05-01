using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebMvcMySql.Models;

namespace WebMvcMySql.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure the database is created and up-to-date
            context.Database.Migrate();

            // Check if the Clients table is empty
            if (context.Clients.Any())
            {
                return; // Database has already been seeded
            }

            // Seed the database with 25 random clients
            var random = new Random();
            var names = new List<string> {
    "Emma", "Noah", "Olivia", "Liam", "Ava", "William", "Sophia", "Mason", "Isabella", "James",
    "Mia", "Benjamin", "Charlotte", "Jacob", "Abigail", "Michael", "Emily", "Elijah", "Harper",
    "Ethan", "Amelia", "Alexander", "Evelyn", "Daniel", "Avery"
};

            var clients = Enumerable.Range(1, 25).Select(i =>
            {
                var name = names[random.Next(names.Count)];
                return new Client
                {
                    Name = $"{name}",
                    Email = $"{name.ToLower()}@example.com",
                    Phone = $"(41) 995{i.ToString().PadLeft(2, '0')}-00{i.ToString().PadLeft(2, '0')}",
                    RegistrationDate = DateTime.Now.AddDays(-random.Next(1, 365)),
                    CPF_CNPJ = random.Next(2) == 0 ? "123.456.789-10" : "12.345.678/0001-21",
                    IsFree = random.Next(2) == 0,
                    IsBlocked = random.Next(2) == 0,
                    TypePerson = random.Next(2) == 0 ? TypePerson.Individual : TypePerson.Corporate,
                    StateDoc = random.Next(2) == 0 ? "1234567890" : null,
                    BirthDate = random.Next(2) == 0 ? (DateTime?)DateTime.Now.AddYears(-random.Next(18, 65)) : null,
                    Gender = random.Next(3) == 0 ? Gender.Male : (random.Next(2) == 0 ? Gender.Female : Gender.Other)
                };
            }).ToList();




            context.Clients.AddRange(clients);
            context.SaveChanges();
        }
    }
}
