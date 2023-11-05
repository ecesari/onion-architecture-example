using CoolBlue.Products.Application.Common;
using CoolBlue.Products.Application.Product.Models;
using CoolBlue.Products.Application.ProductType.Models;
using CoolBlue.Products.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace CoolBlue.Products.Infrastructure.Integration
{
    public class ProductDataIntegration : IProductDataIntegration
    {
        private readonly ProductApiSettings _productApiSettings;
        private readonly IHttpService _httpService;

        public ProductDataIntegration(IOptions<ProductApiSettings> productApiSettings, IHttpService httpService)
        {
            _productApiSettings = productApiSettings.Value;
            _httpService = httpService;
        }

        public async Task<ProductTypeViewModel> GetProductTypeByProductAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _httpService.CallAsync<ProductViewModel>(HttpMethod.Get, _productApiSettings.ProductApiUrl, $"products/{productId}");

            var productTypeId = product.ProductTypeId;
            var productType = await _httpService.CallAsync<ProductTypeViewModel>(HttpMethod.Get, _productApiSettings.ProductApiUrl, $"product_types/{productTypeId}");

            var returnModel = new ProductTypeViewModel
            {
                HasInsurance = productType.HasInsurance,
                Name = productType.Name,
                Id = productTypeId,
            };

            return returnModel;
        }

        public async Task<double> GetSalesPriceAsync(int productId, CancellationToken cancellationToken)
        {
            var product = await _httpService.CallAsync<ProductViewModel>(HttpMethod.Get, _productApiSettings.ProductApiUrl, $"products/{productId}");
            return product.SalesPrice;
        }
    }
}
