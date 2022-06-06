using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    public class Ticket
    {
        [Key]
        [Required]
        public int ticketId { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [ForeignKey("[priorityId]")]
        public int priorityId { get; set; }
        public Priority priority { get; set; }

        [ForeignKey("[assignedEmployeeemployeeId]")]
        public int assignedEmployeeemployeeId { get; set; }
        public Employee? assignedEmployee { get; set; }

        [ForeignKey("[reporteremployeeId]")]
        public int reporteremployeeId { get; set; }
        public Employee? reporter { get; set; }
        public DateTime created { get; set; }
    }
}
