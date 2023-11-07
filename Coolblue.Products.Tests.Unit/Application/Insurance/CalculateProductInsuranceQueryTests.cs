using CoolBlue.Products.Application.Common.Interfaces;
using CoolBlue.Products.Application.Insurance.Queries.CalculateProductInsurance;
using Moq;

namespace Coolblue.Products.Tests.Application.Insurance
{
    public class CalculateProductInsuranceQueryTests
    {
        private readonly CalculateProductInsuranceQueryHandler _uut;
        private readonly Mock<IInsuranceService> _insuranceServiceMock;

        public CalculateProductInsuranceQueryTests()
        {
            _insuranceServiceMock = new Mock<IInsuranceService>();
            _uut = new CalculateProductInsuranceQueryHandler(_insuranceServiceMock.Object);
        }

        [Theory]
        [InlineData(1, 700)]
        [InlineData(5, 500)]
        [InlineData(33, 4897)]
        [InlineData(28, 125)]
        public async void GivenProductId_ShouldReturnInsuranceValue(int productId, double insuranceValue)
        {
            //setup
            _insuranceServiceMock.Setup(x => x.CalculateInsurance(productId, It.IsAny<CancellationToken>())).Returns(Task.FromResult(insuranceValue));
            var query = new CalculateProductInsuranceQuery { ProductId = productId };

            //act
            var insuranceViewModel = await _uut.Handle(query, CancellationToken.None);

            //assert
            Assert.Equal(
                expected: insuranceValue,
                actual: insuranceViewModel.InsuranceValue
            );
        }
    }
}
