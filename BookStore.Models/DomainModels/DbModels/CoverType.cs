using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DomainModels.DbModels
{
    public class CoverType : BaseDbModel
    {
        [Display(Name = "Cover Type")]
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
