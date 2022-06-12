using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model
{
    [Table("Comments")]

    public class Comment
    {
        [Key]
        [Required]
        public int commentId { get; set; }

        public string content { get; set; }

        [ForeignKey("[assignedEmployeeemployeeId]")]

        public int? assignedEmployeeemployeeId { get; set; }

        public virtual Employee? assignedEmployee { get; set; }

        public DateTime created { get; set; }
    }
}
