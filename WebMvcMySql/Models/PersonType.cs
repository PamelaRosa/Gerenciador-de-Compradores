using System.ComponentModel.DataAnnotations;

namespace WebMvcMySql.Models
{
    public enum PersonTypeEnum : byte
    {
        Individual = 1,
        Corporate = 2
    }
    public class PersonType
    {
        [Key]
        public int PersonTypeId { get; set; }

        [StringLength(maximumLength: 50)]
        public PersonTypeEnum? Type { get; set; }

        public ICollection<Buyer>? Buyers { get; set; }
    }
}
