using CoolBlue.Products.Application.Common.Interfaces;
using CoolBlue.Products.Application.Insurance.Queries.CalculateProductInsurance;
using Moq;

namespace Coolblue.Products.Tests.Application.Insurance
{
    public class CalculateOrderInsuranceQueryTests
    {
        private readonly CalculateOrderInsuranceQueryHandler _uut;
        private readonly Mock<IInsuranceService> _insuranceServiceMock;

        public CalculateOrderInsuranceQueryTests()
        {
            _insuranceServiceMock = new Mock<IInsuranceService>();
            _uut = new CalculateOrderInsuranceQueryHandler(_insuranceServiceMock.Object);
        }

        [MemberData(nameof(OrderData))]
        public async void GivenProductList_ShouldReturnTotalCost(List<int> productIds, float insuranceTotal)
        {
            //setup
            var query = new CalculateOrderInsuranceQuery { ProductIds = new List<int>() };

            //act
            var insuranceViewModel = await _uut.Handle(query, CancellationToken.None);

            //assert
            Assert.Equal(
                expected: insuranceTotal,
                actual: insuranceViewModel.InsuranceValue
            );
        }

        public static IEnumerable<object[]> OrderData()
        {
            yield return new object[] { new List<int> { 42, 2112, 54, 33 }, 5487.84 };
            yield return new object[] { new List<int> { 33, 128, 5 }, 184.8 };
            yield return new object[] { new List<int> { 3, 9 }, 4843.5 };
        }
    }
}
