using InspirePO.Utility;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspirePO.Models
{
    [Table(nameof(FormCategory))]
    public class FormCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(nameof(CategoryId))]
        public long CategoryId { get; set; }

        [Required]
        [StringLength(DBConstants.CategoryNameLength, MinimumLength = 3)]
        [Column(nameof(Category))]
        public string Category { get; set; }
    }
}
