using CoolBlue.Products.Application.ProductType.Models;

namespace CoolBlue.Products.Application.Common
{
    public  interface IProductDataIntegration
    {
        ProductTypeViewModel GetProductTypeByProductAsync(int productId, CancellationToken cancellationToken);
        double GetSalesPriceAsync(int productId, CancellationToken cancellationToken);
    }
}
