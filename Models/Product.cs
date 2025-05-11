using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APDS_POE.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FarmerId { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string Category { get; set; }

        public DateTime? DateCreated { get; set; }

        // Navigation property
        [ForeignKey("FarmerId")]
        public int UserId { get; set; }

        public DateTime? ProductionDate { get; set; }
    }
    
}
