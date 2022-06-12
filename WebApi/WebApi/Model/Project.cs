using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    public class Project
    {
        [Key]
        [Required]
        public int projectId { get; set; }

        public String name { get; set; }

        [ForeignKey("[projectManageremployeeId]")]

        public int? projectManageremployeeId { get; set; }
        public virtual Employee? projectManager { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        [ForeignKey("[clientId]")]
        public int? clientId { get; set; }
        public virtual Client? client { get; set; }
    }
}
