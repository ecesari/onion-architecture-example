using Coolblue.Products.Tests.Common.Attributes;
using Coolblue.Products.Tests.Common.Models;
using CoolBlue.Products.Application.Common.Interfaces;
using CoolBlue.Products.Application.Common.Services;
using CoolBlue.Products.Application.ProductType.Models;
using Moq;

namespace Coolblue.Products.Tests.Unit.Application.Services
{
    public class InsuranceServiceTests
    {
        private readonly InsuranceService _uut;
        private readonly Mock<IProductDataIntegrationServices> _productDataIntegrationMock;

        public InsuranceServiceTests()
        {
            _productDataIntegrationMock = new Mock<IProductDataIntegrationServices>();
            _uut = new InsuranceService(_productDataIntegrationMock.Object);
        }
        [Theory]
        [InlineData(1, "Foo", 700, 1000)]
        [InlineData(1, "Laptops", 400, 500)]
        [InlineData(1, "Laptops", 1200, 1500)]
        [InlineData(1, "Foo", 2500, 2000)]
        public async void GivenSalesPrice_ShouldAddToInsuranceCost(int productId, string productTypeName, double salesPrice, double expectedValue)
        {
            //setup
            var productViewModel = new ProductTypeViewModel { HasInsurance = true, Id = productId, Name = productTypeName };
            _productDataIntegrationMock.Setup(x => x.GetProductTypeByProductAsync(productId, It.IsAny<CancellationToken>())).Returns(Task.FromResult(productViewModel));
            _productDataIntegrationMock.Setup(x => x.GetSalesPriceAsync(productId, It.IsAny<CancellationToken>())).Returns(Task.FromResult(salesPrice));

            //act
            var insuranceValue = await _uut.CalculateInsurance(productId, CancellationToken.None);

            //assert
            Assert.Equal(
                expected: expectedValue,
                actual: insuranceValue
            );
        }

        [Theory]
        [OrderModelData]
        public async void GivenBatchSalesPrice_ShouldAddToInsuranceCost(OrderTestModel testModel)
        {
            //setup
            foreach (var line in testModel.OrderLines)
            {
                var productTypeViewModel = new ProductTypeViewModel { HasInsurance = true, Id = line.ProductTypeId };
                _productDataIntegrationMock.Setup(x => x.GetProductTypeByProductAsync(line.Id, It.IsAny<CancellationToken>())).Returns(Task.FromResult(productTypeViewModel));
                _productDataIntegrationMock.Setup(x => x.GetSalesPriceAsync(line.Id, It.IsAny<CancellationToken>())).Returns(Task.FromResult(line.SalesPrice));
            }
            var productIdList = testModel.OrderLines.Select(x=> x.Id).ToList();

            //act
            var insuranceValue = await _uut.CalculateBatchInsurance(productIdList, CancellationToken.None);

            //assert
            Assert.Equal(
                expected: testModel.InsuranceTotal,
                actual: insuranceValue
            );
        }
    }
}
