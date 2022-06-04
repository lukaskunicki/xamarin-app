using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    public class Client
    {
        [Key]
        [Required]
        public int clientId { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [ForeignKey("[responsibleEmployeeemployeeId]")]
        public int? responsibleEmployeeemployeeId { get; set; }
        public virtual Employee? responsibleEmployee { get; set; }

    }
}
