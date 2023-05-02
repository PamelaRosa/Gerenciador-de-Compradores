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
            // Certifique-se de que o banco de dados foi criado e atualizado
            context.Database.Migrate();

            // Verifique se a tabela Clientes está vazia
            if (context.Clients.Any())
            {
                return; // O banco de dados já foi propagado
            }

            // Popule o banco de dados com 25 clientes aleatórios
            var random = new Random();
            var names = new List<string> {
        "Emma", "Noah", "Olivia", "Liam", "Ava", "William", "Sophia", "Mason", "Isabella", "James",
        "Mia", "Benjamin", "Charlotte", "Jacob", "Abigail", "Michael", "Emily", "Elijah", "Harper",
        "Ethan", "Amelia", "Alexander", "Evelyn", "Daniel", "Avery","Pamela","Luana","Jolie","Maria","Rosa"
    };

            var usedNames = new HashSet<string>();
            var clients = Enumerable.Range(1, 25).Select(i =>
            {
                string name;
                do
                {
                    name = names[random.Next(names.Count)];
                } while (usedNames.Contains(name));
                usedNames.Add(name);

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