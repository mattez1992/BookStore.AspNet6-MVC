using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.DomainModels.DbModels
{
    public class Author : BaseDbModel
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [ValidateNever]
        public virtual List<Product> Products { get; set; }
        [Display(Name = "Name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}