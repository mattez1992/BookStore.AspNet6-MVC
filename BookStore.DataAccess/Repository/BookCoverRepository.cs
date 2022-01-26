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
    public class BookCoverRepository : Repository<CoverType>, IBookCoverRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BookCoverRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Update(CoverType cover)
        {
            _dbContext.CoverTypes.Update(cover);
            return Task.CompletedTask;
        }
    }
}
