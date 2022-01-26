using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models.DomainModels.DbModels;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _dbContext;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork dbContext, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            ProductViewModel productView = new()
            {
                Product = new(),
                CategoryList = (await _dbContext.Categories.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                CoverTypeList = (await _dbContext.BookCovers.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                AuthorList = (await _dbContext.Authors.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                // create product
                return View(productView);
            }
            else
            {
                productView.Product = await _dbContext.Products.GetFirstOrDefault(u => u.Id == id);
                return View(productView);

            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductViewModel productView, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(wwwRootPath, @"images\products");
                        var extension = Path.GetExtension(file.FileName);

                        if (productView.Product.ImageUrl != null)
                        {
                            // if imga is update remove old image
                            var oldImagePath = Path.Combine(wwwRootPath, productView.Product.ImageUrl.TrimStart('\\'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            file.CopyTo(fileStreams);
                        }
                        productView.Product.ImageUrl = @"\images\products\" + fileName + extension;

                    }
                    if (productView.Product.Id == 0)
                    {
                       await _dbContext.Products.Add(productView.Product);
                    }
                    else
                    {
                       await _dbContext.Products.Update(productView.Product);
                    }
                    await _dbContext.SaveAsync();
                    TempData["success"] = "Product created successfully";
                    return RedirectToAction("Index");
                }
                return View(productView);
            }
            catch
            {
                return View();
            }
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productList = await _dbContext.Products.GetAll(includes:x => x.Include(x => x.CoverType).Include(x => x.Category).Include(x => x.Author));
            return Json(new { data = productList });
        }

        //POST
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var obj = await _dbContext.Products.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            await _dbContext.Products.Remove(obj);
            await _dbContext.SaveAsync();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion

    }
}
