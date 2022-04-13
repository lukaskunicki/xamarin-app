using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    public class Priority
    {
        [Key]
        [Required]
        public int priorityId { get; set; }

        [StringLength(70)]
        public String name { get; set; }
    }
}
