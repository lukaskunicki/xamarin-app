using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    public class Client
    {
        [Key]
        [Required]
        public int clientId { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        public Employee? responsibleEmployee { get; set; }
    }
}
