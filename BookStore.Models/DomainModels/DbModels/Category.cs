using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.DomainModels.DbModels
{
    public class Category : BaseDbModel
    {
        [Required]
        public string Name { get; set; }
        [Display(Name = "Display Order")]
        [Range(1,100, ErrorMessage = "Display Order must be between 1 and 100 only.")]
        public int DisplayOrder { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
