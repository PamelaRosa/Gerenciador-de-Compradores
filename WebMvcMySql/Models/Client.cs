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
        [StringLength(maximumLength: 150, MinimumLength = 3, ErrorMessage = "O nome não pode ter mais que 150 caracteres.")]
        [Display(Name = "Nome/Razão Social")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
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

        [Display(Name = "Inscrição Estadual")]
        public string? StateDoc { get; set; }

        //PESSOA FISICA - OBRIGATORIO

        [DataType(DataType.Date, ErrorMessage = "A data de cadastro informada não é válida.")]
        [Display(Name = "Data de Nascimento")]
        public DateTime? BirthDate { get; set; }

        //[Required(ErrorMessage = "O Tipo de Pessoa é obrigatório.")]
        [Display(Name = "Gênero")]
        public Gender? Gender { get; set; }

    }

}
