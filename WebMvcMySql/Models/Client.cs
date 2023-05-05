using ExpressiveAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;


namespace WebMvcMySql.Models
{
    public enum TypePerson
    {
        [Display(Name = "Pessoa Física")]
        Individual,
        [Display(Name = "Pessoa Jurídica")]
        Corporate
    }

    public enum Gender
    {
        [Display(Name = "Masculino")]
        Male,
        [Display(Name = "Feminino")]
        Female,
        [Display(Name = "Outro")]
        Other
    }


    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(maximumLength: 150, MinimumLength = 3, ErrorMessage = "O nome não pode ter menos que 3 caracteres e mais que 150 caracteres.")]
        [Display(Name = "Nome/Razão Social")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        [UniqueAttribute(ErrorMessage = "Este endereço de e-mail já está sendo usado por outro cliente.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "O telefone informado não é válido.")]
        [StringLength(maximumLength: 15, MinimumLength = 14, ErrorMessage = "O telefone deve ter no mínimo 10 caracteres e no máximo 11.")]
        [Display(Name = "Telefone")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "A data de cadastro é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "A data de cadastro informada não é válida.")]
        [Display(Name = "Data de Cadastro")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "O CPF/CNPJ é obrigatório.")]
        [UniqueAttribute(ErrorMessage = "Este CPF/CNPJ já está sendo usado por outro cliente.")]
        [Display(Name = "CPF/CNPJ")]
        public string? CPF_CNPJ { get; set; }

        [Display(Name = "Isento")]
        public bool IsFree { get; set; }

        [Display(Name = "Bloqueado")]
        public bool IsBlocked { get; set; }


        [Display(Name = "Inscrição Estadual - Pessoa Física")]
        public bool IsStateDocIndividual { get; set; }

        [Required(ErrorMessage = "O Tipo de Pessoa é obrigatório.")]
        [Display(Name = "Tipo de Pessoa")]
        public TypePerson TypePerson { get; set; }

        [MaxLength(20)]
        [UniqueAttribute(ErrorMessage = "Esta Inscrição Estadual já está sendo usado por outro cliente.")]
        [Display(Name = "Inscrição Estadual")]
        public string? StateDoc { get; set; }

        //PESSOA FISICA - OBRIGATORIO

        [DataType(DataType.Date, ErrorMessage = "A data de cadastro informada não é válida.")]
        [Display(Name = "Data de Nascimento")]
        [CustomValidation(typeof(Client), "ValidateBirthDate")]
        public DateTime? BirthDate { get; set; }

        //[Required(ErrorMessage = "O Tipo de Pessoa é obrigatório.")]
        [Display(Name = "Gênero")]
        [CustomValidation(typeof(Client), "ValidateGender")]
        public Gender? Gender { get; set; }

        //Validações Gênero e Data de Nascimento - obrigatório somente para pessoa física
        public static ValidationResult? ValidateGender(Gender? gender, ValidationContext context)
        {
            var instance = context.ObjectInstance as Client;
            if (instance != null && instance.TypePerson == TypePerson.Individual)
            {
                if (gender == null || (gender != Models.Gender.Male && gender != Models.Gender.Female && gender != Models.Gender.Other))
                {
                    return new ValidationResult("O Gênero é obrigatório (Pessoa Física).");
                }
            }
            return ValidationResult.Success;
        }

        public static ValidationResult? ValidateBirthDate(DateTime? birthDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as Client;
            if (instance != null && instance.TypePerson == TypePerson.Individual)
            {
                if (birthDate == null || birthDate > DateTime.Today)
                {
                    return new ValidationResult("A Data de nascimento é obrigatória (Pessoa Física).");
                }
            }
            return ValidationResult.Success;
        }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Senha deve ter entre 8 e 15 caracteres.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirmação de Senha é obrigatória.")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Confirmação de Senha deve ter entre 8 e 15 caracteres.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "As senhas não conferem.")]
        [Display(Name = "Confirmação da Senha")]
        public string? ConfirmPassword { get; set; }

        [Display(Name = "Excluído")]
        public bool Excluded { get; set; }

    }

}
