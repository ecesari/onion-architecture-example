﻿using CoolBlue.Products.Application.Common.Interfaces;
using CoolBlue.Products.Application.Product.Models;
using CoolBlue.Products.Application.ProductType.Models;
using CoolBlue.Products.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace CoolBlue.Products.Infrastructure.Integration
{
    public class ProductDataIntegrationService : IProductDataIntegrationService
    {
        private readonly ProductApiSettings _productApiSettings;
        private readonly IHttpService _httpService;

        public ProductDataIntegrationService(IOptions<ProductApiSettings> productApiSettings, IHttpService httpService)
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
                CanBeInsured = productType.CanBeInsured,
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
