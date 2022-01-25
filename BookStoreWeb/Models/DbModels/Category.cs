using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreWeb.Models.DbModels
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
