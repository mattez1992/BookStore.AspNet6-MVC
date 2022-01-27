using BookStore.DataAccess.Data;
using BookStore.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public ICategoryRepository Categories {get; private set;}

        public IBookCoverRepository BookCovers { get; private set; }

        public IProductRepository Products { get; private set; }

        public IAuthorRepository Authors { get; private set; }

        public ICompanyRepository Companies { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            Categories = new CategoryRepository(dbContext);
            BookCovers = new BookCoverRepository(dbContext);
            Products = new ProductRepository(dbContext);
            Authors = new AuthorRepository(dbContext);
            Companies = new CompnayRepository(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
