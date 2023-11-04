using CoolBlue.Products.Application.Common;
using CoolBlue.Products.Application.Insurance.Queries.CalculateInsurance;
using Moq;

namespace Coolblue.Products.Tests.Unit.Application.Insurance
{
    public class CalculateInsuranceQueryHandlerTests
    {
        private readonly CalculateInsuranceQueryHandler _calculateInsuranceQueryHandler;
        private readonly Mock<IProductDataIntegration> _productDataIntegrationMock;

        public CalculateInsuranceQueryHandlerTests()
        {
            _productDataIntegrationMock = new Mock<IProductDataIntegration>();
            _calculateInsuranceQueryHandler = new CalculateInsuranceQueryHandler(_productDataIntegrationMock.Object);
        }

        [Theory]
        [InlineData(1, 700,1000)]
        [InlineData(1, 700,500)]
        public async void CalculateInsuranceQuery_Handle_GivenSalesPriceBetween500And2000Euros_ShouldAddThousandEurosToInsuranceCost(int productId, float salesPrice, double expectedValue)
        {
            _productDataIntegrationMock.Setup(x => x.GetProductTypeByProductAsync(productId, It.IsAny<CancellationToken>())).Returns(new CoolBlue.Products.Application.ProductType.Models.ProductTypeViewModel { HasInsurance = true, Id = productId, Name = "Foo"});
            _productDataIntegrationMock.Setup(x => x.GetSalesPriceAsync(productId, It.IsAny<CancellationToken>())).Returns(salesPrice);
            var query = new CalculateInsuranceQuery { ProductId = productId };

            //act
            var insuranceViewModel = await _calculateInsuranceQueryHandler.Handle(query, CancellationToken.None);

            Assert.Equal(
                expected: expectedValue,
                actual: insuranceViewModel.InsuranceValue
            );

        }
    }
}
