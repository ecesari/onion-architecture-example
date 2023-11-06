using CoolBlue.Products.Domain.Entities;
using CoolBlue.Products.Domain.Specifications;

namespace CoolBlue.Products.Domain.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(IBaseSpecification<T> spec);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
