using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models.DomainModels.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _dbContext;

        public CategoryController(IUnitOfWork dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categoryList = await _dbContext.Categories.GetAll();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Displayorder cannot exactly match the Name.");
                return View(category);
            }
            if (ModelState.IsValid)
            {
                await _dbContext.Categories.Add(category);
                await _dbContext.SaveAsync();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var category = await _dbContext.Categories.GetFirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category categoryToEdit)
        {
            if (categoryToEdit.Name == categoryToEdit.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Displayorder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
               await  _dbContext.Categories.Update(categoryToEdit);
                await _dbContext.SaveAsync();
                TempData["success"] = "Category edit successfully";
                return RedirectToAction("Index");
            }
            return View(categoryToEdit);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var categoryToDelete = await _dbContext.Categories.GetFirstOrDefault(x => x.Id == id);
            if (categoryToDelete == null)
            {
                return NotFound();
            }

            return View(categoryToDelete);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category categoryToDelete)
        {
            await _dbContext.Categories.Remove(categoryToDelete);

            await _dbContext.SaveAsync();
            TempData["success"] = "Category removed successfully";
            return RedirectToAction("Index");
        }
    }
}
