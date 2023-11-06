using CoolBlue.Products.Domain.Entities;
using CoolBlue.Products.Domain.Repositories;
using CoolBlue.Products.Infrastructure.Data;
using CoolBlue.Products.Infrastructure.Repository.Base;

namespace CoolBlue.Products.Infrastructure.Repository
{
    public class ProductTypeRepository : BaseRepository<ProductType>, IProductTypeRepository
    {
        public ProductTypeRepository(InsuranceDbContext insuranceDbContext) : base(insuranceDbContext)
        {

        }
    }
}
