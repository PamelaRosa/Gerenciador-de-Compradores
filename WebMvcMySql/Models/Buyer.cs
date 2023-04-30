using ExpressiveAnnotations.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMvcMySql.Models
{
    public class Buyer
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("PersonType")]
        public int PersonTypeId { get; set; }

        [Required(ErrorMessage = "O Tipo de Pessoa é obrigatório.")]
        [Display(Name = "Tipo de Pessoa")]
        public PersonTypeEnum? Type { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(maximumLength: 150, MinimumLength = 2, ErrorMessage = "O nome não pode ter mais que 100 caracteres.")]
        [Display(Name = "Nome/Razão Social")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        [Display(Name = "E-mail")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [Phone(ErrorMessage = "O telefone informado não é válido.")]
        [StringLength(maximumLength: 11, MinimumLength = 10, ErrorMessage = "O telefone deve ter no mínimo 10 caracteres e no máximo 11.")]
        [Display(Name = "Telefone")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "A data de cadastro é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "A data de cadastro informada não é válida.")]
        [Display(Name = "Data de Cadastro")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        //---------------------
        //PESSOA FISICA - OBRIGATORIO

        [Required(ErrorMessage = "O campo CPF/CNPJ é obrigatório.")]
        [Display(Name = "CPF/CNPJ")]
        public string? CPF_CNPJ { get; set; }

        [RequiredIf("PersonTypeId == 1", ErrorMessage = "O campo Gênero é obrigatório.")]
        [Display(Name = "Gênero")]
        public string? Gender { get; set; }

        [RequiredIf("PersonTypeId == 1", ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "A data de cadastro informada não é válida.")]
        [Display(Name = "Data de Nascimento")]
        public DateTime? BirthDate { get; set; }

        // PESSOA JURIDICA - OBRIGATORIO

        [RequiredIf("PersonTypeId == 2", ErrorMessage = "O campo CNPJ é obrigatório")]
        [Display(Name = "Inscrição Estadual")]
        public string? StateDoc { get; set; }

        [Display(Name = "Isento")]
        public bool IsFree { get; set; }
        //---------------------

        [Display(Name = "Bloqueado")]
        public bool IsBlocked { get; set; }


    }

}
