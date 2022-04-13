using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApi.Model
{

    [Table("Teams")]
    public class Team
    {
        [Key]
        [Required]
        public int teamId { get; set; }
        [StringLength(70)]
        public String teamName { get; set; }
        [StringLength(200)]
        public String teamDescription { get; set; }
    }
}