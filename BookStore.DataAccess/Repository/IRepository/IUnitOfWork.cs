using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IBookCoverRepository BookCovers { get; }
        IProductRepository Products { get; }
        IAuthorRepository Authors { get; }
        ICompanyRepository Companies { get; }
        Task<int> SaveAsync();
    }
}
