using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebMvcMySql.Data;

namespace WebMvcMySql.Models
{
    public class UniqueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value is not string email || value is not string cpf_cnpj || value is not string stateDoc)
            {
                return ValidationResult.Success; 
            }

            var db = validationContext.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;

            var client = (Client)validationContext.ObjectInstance;
            var existingClient = db?.Clients.FirstOrDefault(c => c.Email == email || c.CPF_CNPJ == cpf_cnpj || c.StateDoc == stateDoc);

            if (existingClient != null && existingClient.Id != client.Id)
            {
                if (existingClient.Email == email)
                {
                    return new ValidationResult("Este endereço de e-mail já está sendo usado por outro cliente.");
                }
                else if (existingClient.CPF_CNPJ == cpf_cnpj)
                {
                    return new ValidationResult("Este CPF/CNPJ já está sendo usado por outro cliente.");
                }
                else
                {
                    return new ValidationResult("Esta Inscrição Estadual já está sendo usado por outro cliente.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
