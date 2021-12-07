using System.ComponentModel.DataAnnotations;

namespace API.Resources
{
    public class SaveAuthorResource
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
