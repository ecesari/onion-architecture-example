using CoolBlue.Products.Application.ProductType.Models;

namespace CoolBlue.Products.Application.Common
{
    public  interface IProductDataIntegration
    {
        List<ProductTypeViewModel> GetProductTypeAsync(int productId);
        int GetSalesPriceAsync(int productId);
    }
}
