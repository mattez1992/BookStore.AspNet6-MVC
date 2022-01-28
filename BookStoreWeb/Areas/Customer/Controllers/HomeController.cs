
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models.DomainModels.DbModels;
using BookStore.Models.ViewModels;
using BookStore.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace BookStoreWeb.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _dbContext;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _dbContext.Products.GetAll(includes: x => x.Include(x => x.Category).Include(x => x.CoverType).Include(x => x.Author));
            return View(products);
        }
        public async Task<IActionResult> Details(int productId)
        {
            var product = await _dbContext.Products.GetFirstOrDefault(x => x.Id == productId, includes: x => x.Include(x => x.Author).Include(x => x.Category).Include(x => x.CoverType));
            ShoppingCartItem cartItem = new()
            {
                Product = product,
                ProductId = productId,
                Count = 1
            };
            return View(cartItem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Details(ShoppingCartItem cartItem)
        {
            try
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                cartItem.ApplicationUserId = claim.Value;
                // check if a cart exists
                ShoppingCartItem existingCart = await _dbContext.ShoppingCartItems.GetFirstOrDefault(
            u => u.ApplicationUserId == claim.Value && u.ProductId == cartItem.ProductId);

                if (existingCart == null)
                {
                    cartItem.Product = null;
                    await _dbContext.ShoppingCartItems.Add(cartItem);
                   
               //     HttpContext.Session.SetInt32(Constants.SessionCart,
               //(await _dbContext.ShoppingCartItems.GetAll(u => u.ApplicationUserId == claim.Value)).ToList().Count);
                }
                else
                {
                    _dbContext.ShoppingCartItems.IncrementCount(existingCart, cartItem.Count);
                }

                await _dbContext.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error));
            }
           
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}