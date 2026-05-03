using System.Linq.Expressions;

namespace CollegeApp.Data.Repository
{
    public interface ICollegeRepository<T> //this is the generic interface
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(Expression<Func<T,bool>> filter);
        //Task<T> GetByNameAsync(Expression<Func<T, bool>> filter); this is a repititon of GetId method, the filter function is doing the job already
        Task<List<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter);
        Task<T> CreateAsync(T dbRecord);
        Task<T> UpdateAsync(T dbRecord);
        Task<bool> DeleteAsync(T dbRecord);
    }
}
