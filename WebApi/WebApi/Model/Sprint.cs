using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    public class Sprint
    {
        [Key]
        [Required]
        public int sprintId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
    }
}
