using BookStore.Models.DomainModels.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository.IRepository
{
    public interface IBookCoverRepository : IRepository<CoverType>
    {
        Task Update(CoverType cover);
    }
}
