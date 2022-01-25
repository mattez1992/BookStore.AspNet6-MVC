using BookStoreWeb.Data;
using BookStoreWeb.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _dbContext.Categories;
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Displayorder cannot exactly match the Name.");
                return View(category);
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category categoryToEdit)
        {
            if (categoryToEdit.Name == categoryToEdit.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Displayorder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(categoryToEdit);
                _dbContext.SaveChanges();
                TempData["success"] = "Category edit successfully";
                return RedirectToAction("Index");
            }
            return View(categoryToEdit);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category categoryDToDelete)
        {
            _dbContext.Categories.Remove(categoryDToDelete);

            _dbContext.SaveChanges();
            TempData["success"] = "Category removed successfully";
            return RedirectToAction("Index");
        }
    }
}
