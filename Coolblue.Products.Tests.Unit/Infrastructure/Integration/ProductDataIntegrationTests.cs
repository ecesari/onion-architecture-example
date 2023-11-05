using Coolblue.Products.Common.Settings;
using CoolBlue.Products.Application.Common;
using CoolBlue.Products.Application.Product.Models;
using CoolBlue.Products.Application.ProductType.Models;
using CoolBlue.Products.Infrastructure.Integration;
using Microsoft.Extensions.Options;
using Moq;

namespace Coolblue.Products.Tests.Unit.Infrastructure.Integration
{
    public class ProductDataIntegrationTests
    {
        private readonly Mock<IOptions<ProductApiSettings>> _productApiSettingsMock;
        private readonly Mock<IHttpService> _httpServiceMock;
        private readonly ProductDataIntegration _uut;


        public ProductDataIntegrationTests()
        {
            _productApiSettingsMock = new Mock<IOptions<ProductApiSettings>>();
            _productApiSettingsMock.Setup(x => x.Value).Returns(new ProductApiSettings());

            _httpServiceMock = new Mock<IHttpService>();
            _uut = new ProductDataIntegration(_productApiSettingsMock.Object, _httpServiceMock.Object);
        }

        [Theory]
        [InlineData(1, 1, true, "Laptops", 700)]
        public async void GivenProductId_ShouldReturnProductType(int productId, int productTypeId, bool hasInsurance, string productTypeName, double salesPrice)
        {
            //setup
            var productViewModel = new ProductViewModel { ProductId = productId, SalesPrice = salesPrice };
            var productTypeViewModel = new ProductTypeViewModel { Id = 0, HasInsurance = hasInsurance, Name = productTypeName };
            _httpServiceMock.Setup(x => x.CallAsync<ProductViewModel>(HttpMethod.Get, _productApiSettingsMock.Object.Value.ProductApiUrl, $"products/{productId}")).Returns(Task.FromResult(productViewModel));
            _httpServiceMock.Setup(x => x.CallAsync<ProductTypeViewModel>(HttpMethod.Get, _productApiSettingsMock.Object.Value.ProductApiUrl, $"product_types/{productTypeId}")).Returns(Task.FromResult(productTypeViewModel));

            //act
            var returnModel = _uut.GetProductTypeByProductAsync(productId, CancellationToken.None);

            //assert
            Assert.Equal(expected: productTypeId, actual: returnModel.Result.Id);
            Assert.Equal(expected: hasInsurance, actual: returnModel.Result.HasInsurance);
            Assert.Equal(expected: productTypeName, actual: returnModel.Result.Name);
        }


        [Theory]
        [InlineData(1, 700)]
        public async void GivenProductId_ShouldReturnSalesPrice(int productId, double salesPrice)
        {
            //setup
            var productViewModel = new ProductViewModel { ProductId = productId, SalesPrice = salesPrice };
            _httpServiceMock.Setup(x => x.CallAsync<ProductViewModel>(HttpMethod.Get, _productApiSettingsMock.Object.Value.ProductApiUrl, $"products/{productId}")).Returns(Task.FromResult(productViewModel));

            //act
            var returnModel = _uut.GetSalesPriceAsync(productId, CancellationToken.None);

            //assert
            Assert.Equal(expected: salesPrice, actual: returnModel.Result);
        }
    }
}
