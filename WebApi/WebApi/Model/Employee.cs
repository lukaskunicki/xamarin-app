using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApi.Model
{

    [Table("Employees")]
    public class Employee
    {
        [Key]
        [Required]
        public int employeeId { get; set; }
        [StringLength(50)]
        public String name { get; set; }
        [StringLength(50)]
        public String surname { get; set; }

        [ForeignKey("[teamId]")]
        public int? teamId { get; set; }
        public Team team { get; set; }
    }
}