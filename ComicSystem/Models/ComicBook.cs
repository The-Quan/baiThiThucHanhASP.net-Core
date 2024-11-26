using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicSystem.Models
{
    public class ComicBook
    {
        [Key]
        public int ComicBookID { get; set; }

        [Required]
        [MaxLength(255)]  
        public string Title { get; set; }

        [Required]
        [MaxLength(255)]
        public string Author { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerDay { get; set; }
    }
}