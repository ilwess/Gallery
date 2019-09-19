using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<IEnumerable<TModel>>
            GetAsync(Expression<Func<TModel, bool>> predicate);
        Task<TModel> GetAsync(int id);
        Task DeleteAsync(TModel model);
        Task DeleteAsync(int id);
        Task CreateAsync(TModel newModel);
        Task UpdateAsync(TModel model);
    }
}
