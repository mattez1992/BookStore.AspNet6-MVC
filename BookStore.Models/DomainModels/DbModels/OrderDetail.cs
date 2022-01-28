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
    public class OrderDetail :BaseDbModel
    {
        [Required]
        [ForeignKey(nameof(OrderHeader))]
        public int OrderHeaderId { get; set; }       
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }
        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }        
        [ValidateNever]
        public Product Product { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
