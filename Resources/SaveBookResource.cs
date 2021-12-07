using System.ComponentModel.DataAnnotations;

namespace API.Resources
{
    public class SaveBookResource
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0, 100)]
        public int Price { get; set; }
        
        [Required]
        public int AuthorId { get; set; }
    }
}
