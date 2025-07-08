using System.Linq.Expressions;

namespace CollegeApp.Data.Repository
{
    public interface ICollegeRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useAsNoTracking = false);
        Task<T> Create(T dbRecord);
        Task<T> Update(T dbRecord);
        Task<bool> DeleteAsync(T dbRecord);
    }
}
