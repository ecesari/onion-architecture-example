using CoolBlue.Products.Application.Common;
using CoolBlue.Products.Application.Product.Models;
using CoolBlue.Products.Application.ProductType.Models;
using Newtonsoft.Json;

namespace CoolBlue.Products.Infrastructure.Integration
{
    internal class ProductDataIntegration : IProductDataIntegration
    {
        private const string ProductApi = "http://localhost:5002";

        public ProductTypeViewModel GetProductTypeByProductAsync(int productId, CancellationToken cancellationToken)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(ProductApi) };
            string json = client.GetAsync(string.Format("/products/{0:G}", productId), cancellationToken).Result.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<ProductViewModel>(json);

            var productTypeId = product.ProductId;
            json = client.GetAsync(string.Format("/product_types/{0:G}", productTypeId), cancellationToken).Result.Content.ReadAsStringAsync().Result;
            var productType = JsonConvert.DeserializeObject<ProductTypeViewModel>(json);

            var returnModel = new ProductTypeViewModel
            {
                HasInsurance = productType.HasInsurance,
                Name = productType.Name,
                Id = productTypeId,
            };

            return returnModel;
        }



        public float GetSalesPriceAsync(int productId, CancellationToken cancellationToken)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(ProductApi) };
            string json = client.GetAsync(string.Format("/products/{0:G}", productId, cancellationToken)).Result.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<ProductViewModel>(json);

            return product.SalesPrice;
        }
    }
}
