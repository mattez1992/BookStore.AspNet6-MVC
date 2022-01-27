using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.DomainModels.DbModels
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string? StreetAdess { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        [ForeignKey(nameof(Company))]
        public int? CompanyId { get; set; }
        [ValidateNever]
        public Company Company { get; set; }
    }
}
