using InspirePO.Utility;
using System.ComponentModel.DataAnnotations;

namespace InspirePO.DTOs
{
    public class CategoryDTO
    {
        public long? CategoryId { get; set; }

        [Required]
        [StringLength(DBConstants.CategoryNameLength, MinimumLength = 3)]
        public string Category { get; set; }
    }
}
