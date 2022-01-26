using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models.DomainModels.DbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _dbContext;

        public CoverTypeController(IUnitOfWork dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var coverTypes = await _dbContext.BookCovers.GetAll();
            return View(coverTypes);
        }



        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoverType coverType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _dbContext.BookCovers.Add(coverType);
                    await _dbContext.SaveAsync();
                    TempData["success"] = "CoverType created successfully";
                    return RedirectToAction(nameof(Index));
                }
                return View(coverType);
            }
            catch
            {
                return View(coverType);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var coverType = await _dbContext.BookCovers.GetFirstOrDefault(x => x.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CoverType cover)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _dbContext.BookCovers.Update(cover);
                    await _dbContext.SaveAsync();
                    TempData["success"] = "Cover Type edit successfully";
                    return RedirectToAction(nameof(Index));
                }
                return View(cover);
            }
            catch
            {
                return View();
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            var coverToDelete = await _dbContext.BookCovers.GetFirstOrDefault(x => x.Id == id);
            if (coverToDelete == null)
            {
                return NotFound();
            }

            return View(coverToDelete);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CoverType coverToDelete)
        {
            try
            {
                await _dbContext.BookCovers.Remove(coverToDelete);

                await _dbContext.SaveAsync();
                TempData["success"] = "Book Cover removed successfully";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
