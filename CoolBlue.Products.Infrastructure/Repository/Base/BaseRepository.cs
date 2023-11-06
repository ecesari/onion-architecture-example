using CoolBlue.Products.Domain.Entities;
using CoolBlue.Products.Domain.Repositories;
using CoolBlue.Products.Domain.Specifications;
using CoolBlue.Products.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoolBlue.Products.Infrastructure.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly InsuranceDbContext _insuranceDb;

        public BaseRepository(InsuranceDbContext insuranceDb)
        {
            _insuranceDb = insuranceDb;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _insuranceDb.Set<T>().AddAsync(entity);
            await _insuranceDb.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _insuranceDb.Set<T>().Remove(entity);
            await _insuranceDb.SaveChangesAsync();
        }


        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _insuranceDb.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(IBaseSpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _insuranceDb.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _insuranceDb.Entry(entity).State = EntityState.Modified;
            try
            {
                await _insuranceDb.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
        }

        private IQueryable<T> ApplySpecification(IBaseSpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_insuranceDb.Set<T>().AsQueryable(), spec);
        }

    }
}
