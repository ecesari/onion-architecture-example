using Microsoft.EntityFrameworkCore;

namespace CoolBlue.Products.Application.Common.Interfaces
{
    public interface IInsuranceDbContext
    {
        DbSet<Domain.Entities.ProductType> ProductTypes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
