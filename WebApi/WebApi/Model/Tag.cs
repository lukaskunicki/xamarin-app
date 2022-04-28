using System.ComponentModel.DataAnnotations;

namespace WebApi.Model
{
    public class Tag
    {
        [Key]
        [Required]
        public int tagId { get; set; }
        public string description { get; set; }
    }
}
