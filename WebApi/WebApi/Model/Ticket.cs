using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    public class Ticket
    {
        [Key]
        [Required]
        public int ticketId { get; set; }

        [StringLength(255)]
        public string title { get; set; }
        
        public Priority priority { get; set; }

        public Employee? assignedEmployee { get; set; }

        public Employee? reporter { get; set; }

        public DateTime created { get; set; }
    }
}
