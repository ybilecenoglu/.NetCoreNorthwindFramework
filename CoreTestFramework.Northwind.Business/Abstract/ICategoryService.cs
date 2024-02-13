using System.Linq.Expressions;
using CoreTestFramework.Core.Common;
using CoreTestFramework.Northwind.Entities.Model;

namespace CoreTestFramework.Northwind.Business
{
    public interface ICategoryService 
    {
        Task<Result<List<Category>>> GetCategoryListAsync(Expression<Func<Category, bool>> filter = null);
        Task<Result<Category>> GetCategoryAsync(Expression<Func<Category, bool>> filter = null);
        Task<Result> AddCategoryAsync(Category category);
        Task<Result> DeleteCategoryAsync(Category category);
        Task<Result> UpdateCategoryAsync(Category category);
        Task<Result<Category>> FindByIdAsync (int id);
    }
}