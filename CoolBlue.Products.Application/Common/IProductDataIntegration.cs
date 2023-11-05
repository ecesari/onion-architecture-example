using CoolBlue.Products.Application.ProductType.Models;

namespace CoolBlue.Products.Application.Common
{
    public  interface IProductDataIntegration
    {
        Task<ProductTypeViewModel> GetProductTypeByProductAsync(int productId, CancellationToken cancellationToken);
        Task<double> GetSalesPriceAsync(int productId, CancellationToken cancellationToken);
    }
}
