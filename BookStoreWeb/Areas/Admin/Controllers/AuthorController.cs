using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models.DomainModels.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _dbContext;

        public AuthorController(IUnitOfWork dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            Author author = new();
            if (id == null || id == 0)
            {
                // create product
                return View(author);
            }
            else
            {
                author = await _dbContext.Authors.GetFirstOrDefault(u => u.Id == id);
                return View(author);

            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Author author)
        {
            try
            {
                if (ModelState.IsValid)
                {                   
                    if (author.Id == 0)
                    {
                        await _dbContext.Authors.Add(author);
                    }
                    else
                    {
                        await _dbContext.Authors.Update(author);
                    }
                    await _dbContext.SaveAsync();
                    TempData["success"] = "Author created successfully";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        #region API Endpoints
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _dbContext.Authors.GetAll();
            return Json(new { data = authors });
        }

        //POST
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var authorToDelete = await _dbContext.Authors.GetFirstOrDefault(u => u.Id == id);
            if (authorToDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            await _dbContext.Authors.Remove(authorToDelete);
            await _dbContext.SaveAsync();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion

    }
}
