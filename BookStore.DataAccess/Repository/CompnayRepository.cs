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
    public class CompnayRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CompnayRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Update(Company company)
        {
            _dbContext.Update(company);
            return Task.CompletedTask;
        }
    }
}
