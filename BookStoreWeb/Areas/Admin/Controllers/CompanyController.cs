using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models.DomainModels.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _dbContext;

        public CompanyController(IUnitOfWork dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            Company company = new();
            if (id == null || id == 0)
            {
                // create product
                return View(company);
            }
            else
            {
                company = await _dbContext.Companies.GetFirstOrDefault(u => u.Id == id);
                return View(company);

            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Company company)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (company.Id == 0)
                    {
                        await _dbContext.Companies.Add(company);
                    }
                    else
                    {
                        await _dbContext.Companies.Update(company);
                    }
                    await _dbContext.SaveAsync();
                    TempData["success"] = "Company created successfully";
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
            var companies = await _dbContext.Companies.GetAll();
            return Json(new { data = companies });
        }

        //POST
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var companyToDelete = await _dbContext.Companies.GetFirstOrDefault(u => u.Id == id);
            if (companyToDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            await _dbContext.Companies.Remove(companyToDelete);
            await _dbContext.SaveAsync();
            return Json(new { success = true, message = "Delete Successful" });

        }
        #endregion

    }
}

