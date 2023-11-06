using CoolBlue.Products.Application.ProductType.Models;

namespace CoolBlue.Products.Application.Common.Interfaces
{
    public interface IProductDataIntegrationService
    {
        Task<ProductTypeViewModel> GetProductTypeByProductAsync(int productId, CancellationToken cancellationToken);
        Task<double> GetSalesPriceAsync(int productId, CancellationToken cancellationToken);
    }
}
