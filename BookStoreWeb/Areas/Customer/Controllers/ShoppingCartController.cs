using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models.DomainModels.DbModels;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookStoreWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _dbContext;
        public ShoppingCartItemViewModel ShoppingCart { get; set; }
        public double OrderTotal { get; set; }
        public ShoppingCartController(IUnitOfWork _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public async Task<IActionResult> Index()
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                ShoppingCart = new ShoppingCartItemViewModel()
                {
                    ShoppingCartItems = await _dbContext.ShoppingCartItems.GetAll(predicate: x => x.ApplicationUserId == claim.Value,
                    includes: x => x.Include(x => x.Product))
                };
                foreach (var item in ShoppingCart.ShoppingCartItems)
                {
                    item.Price = GetPriceBasedOnQuantity(item);
                    ShoppingCart.CartTotal += item.Price * item.Count;
                }
                return View(ShoppingCart);
            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });          
        }
        public async Task<IActionResult> Summary()
        {
            return View();
        }
        public async Task<IActionResult> IncreaseProductCount(int cartId)
        {
            var cart = await _dbContext.ShoppingCartItems.GetFirstOrDefault(x => x.Id == cartId);
            _dbContext.ShoppingCartItems.IncrementCount(cart, 1);
            await _dbContext.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DecreaseProductCount(int cartId)
        {
            var cart = await _dbContext.ShoppingCartItems.GetFirstOrDefault(x => x.Id == cartId);
            if(cart.Count <= 1)
            {
                await _dbContext.ShoppingCartItems.Remove(cart);
            }
            else
            {
                _dbContext.ShoppingCartItems.DecrementCount(cart, 1);
            }
            
            await _dbContext.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveProductFromCart(int cartId)
        {
            var cart = await _dbContext.ShoppingCartItems.GetFirstOrDefault(x => x.Id == cartId);
            
            await _dbContext.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        private double GetPriceBasedOnQuantity(ShoppingCartItem shoppingCart)
        {
            switch (shoppingCart.Count)
            {
                case <= 50:
                    return shoppingCart.Product.Price;
                case <= 100:
                    return shoppingCart.Product.Price50;
                case > 100:
                    return shoppingCart.Product.Price100;
            }
        }
    }
}
