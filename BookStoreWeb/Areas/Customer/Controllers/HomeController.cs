
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models.DomainModels.DbModels;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
        public async Task<IActionResult> Details(int id)
        {
            var product = await _dbContext.Products.GetFirstOrDefault(x => x.Id == id, includes: x => x.Include(x => x.Author).Include(x => x.Category).Include(x => x.CoverType));
            ShoppingCartItem cartItem = new()
            {
                Product = product,
                Count = 1
            };
            return View(cartItem);
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