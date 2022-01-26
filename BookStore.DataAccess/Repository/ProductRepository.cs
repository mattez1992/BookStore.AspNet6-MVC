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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Update(Product editedProduct)
        {
            var productTOEdit = _dbContext.Products.FirstOrDefault(u => u.Id == editedProduct.Id);
            if (productTOEdit != null)
            {
                productTOEdit.Title = editedProduct.Title;
                productTOEdit.ISBN = editedProduct.ISBN;
                productTOEdit.Price = editedProduct.Price;
                productTOEdit.Price50 = editedProduct.Price50;
                productTOEdit.ListPrice = editedProduct.ListPrice;
                productTOEdit.Price100 = editedProduct.Price100;
                productTOEdit.Description = editedProduct.Description;
                productTOEdit.CategoryId = editedProduct.CategoryId;
                productTOEdit.AuthorId = editedProduct.AuthorId;
                productTOEdit.CoverTypeId = editedProduct.CoverTypeId;
                if (editedProduct.ImageUrl != null)
                {
                    productTOEdit.ImageUrl = editedProduct.ImageUrl;
                }
            }
            return Task.CompletedTask;
        }
    }
}
