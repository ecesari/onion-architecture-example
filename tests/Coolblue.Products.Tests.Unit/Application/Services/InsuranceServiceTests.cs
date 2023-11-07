using Coolblue.Products.Tests.Common.Attributes;
using Coolblue.Products.Tests.Common.Models;
using CoolBlue.Products.Application.Common.Interfaces;
using CoolBlue.Products.Application.Common.Services;
using CoolBlue.Products.Application.ProductType.Models;
using CoolBlue.Products.Domain.Entities;
using CoolBlue.Products.Domain.Repositories;
using Moq;

namespace Coolblue.Products.Tests.Application.Services
{
    public class InsuranceServiceTests
    {
        private readonly InsuranceService _uut;
        private readonly Mock<IProductDataIntegrationService> _productDataIntegrationMock;
        private readonly Mock<IProductTypeRepository> _productTypeRepositoryMock;

        public InsuranceServiceTests()
        {
            _productDataIntegrationMock = new Mock<IProductDataIntegrationService>();
            _productTypeRepositoryMock = new Mock<IProductTypeRepository>();
            _uut = new InsuranceService(_productDataIntegrationMock.Object, _productTypeRepositoryMock.Object);
        }
        [Theory]
        [InlineData(1, "Foo", 700, 1000)]
        [InlineData(1, "Laptops", 400, 500)]
        [InlineData(1, "Laptops", 1200, 1500)]
        [InlineData(1, "Foo", 2500, 2000)]
        public async void GivenSalesPrice_ShouldAddToInsuranceCost(int productId, string productTypeName, double salesPrice, double expectedValue)
        {
            //setup
            var productViewModel = new ProductTypeViewModel { CanBeInsured = true, Id = productId, Name = productTypeName };
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
                var productTypeViewModel = new ProductTypeViewModel { CanBeInsured = true, Id = line.ProductTypeId };
                _productDataIntegrationMock
                    .Setup(x => x.GetProductTypeByProductAsync(line.Id, It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(productTypeViewModel));
                _productDataIntegrationMock
                    .Setup(x => x.GetSalesPriceAsync(line.Id, It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(line.SalesPrice));
            }
            var productIdList = testModel.OrderLines.Select(x => x.Id).ToList();

            //act
            var insuranceValue = await _uut.CalculateBatchInsurance(productIdList, CancellationToken.None);

            //assert
            Assert.Equal(
                expected: testModel.InsuranceTotal,
                actual: insuranceValue
            );
        }

        [Theory]
        [OrderModelData]
        public async void GivenBatchSalesPrice_WithCamera_ShouldAddToInsuranceCost(OrderTestModel testModel)
        {
            //setup
            foreach (var line in testModel.OrderLines)
            {
                var productTypeViewModel = new ProductTypeViewModel { CanBeInsured = true, Id = line.ProductTypeId, Name = "Digital cameras" };
                _productDataIntegrationMock
                    .Setup(x => x.GetProductTypeByProductAsync(line.Id, It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(productTypeViewModel));
                _productDataIntegrationMock
                    .Setup(x => x.GetSalesPriceAsync(line.Id, It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(line.SalesPrice));
            }
            var productIdList = testModel.OrderLines.Select(x => x.Id).ToList();
            var expectedInsuranceValue = testModel.InsuranceTotal + 500;

            //act
            var insuranceValue = await _uut.CalculateBatchInsurance(productIdList, CancellationToken.None);

            //assert
            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: insuranceValue
            );
        }     
    }
}
