using BookStore.Models.DomainModels.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository.IRepository
{
    public interface IShoppingCartItemRepository : IRepository<ShoppingCartItem>
    {
        Task Update(ShoppingCartItem item);
        int IncrementCount(ShoppingCartItem shoppingCartItem, int count);
        int DecrementCount(ShoppingCartItem shoppingCartItem, int count);
    }
}
