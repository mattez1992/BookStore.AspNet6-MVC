using BookStore.Models.DomainModels.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.ViewModels
{
    public class ShoppingCartItemViewModel
    {
        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }
        public double CartTotal { get; set; }
        //public OrderHeader OrderHeader { get; set; }
    }
}
