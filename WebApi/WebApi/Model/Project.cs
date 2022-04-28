using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    public class Project
    {
        [Key]
        [Required]
        public int projectId { get; set; }

        public String name { get; set; }
        public Employee projectManager { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public Client client { get; set; }
    }
}
