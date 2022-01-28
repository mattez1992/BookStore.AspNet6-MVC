using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models.DomainModels.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class ShoppingCartItemRepository : Repository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ShoppingCartItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int DecrementCount(ShoppingCartItem shoppingCartItem, int count)
        {
            shoppingCartItem.Count -= count;
            return shoppingCartItem.Count;
        }

        public int IncrementCount(ShoppingCartItem shoppingCartItem, int count)
        {
            shoppingCartItem.Count += count;
            return shoppingCartItem.Count;
        }

        public Task Update(ShoppingCartItem item)
        {
           _dbContext.ShoppingCartItems.Add(item);
            return Task.CompletedTask;
        }
    }
}
