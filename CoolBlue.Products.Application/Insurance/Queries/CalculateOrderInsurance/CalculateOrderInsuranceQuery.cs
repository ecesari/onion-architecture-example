using CoolBlue.Products.Application.Common.Interfaces;
using CoolBlue.Products.Application.Insurance.Models;
using MediatR;

namespace CoolBlue.Products.Application.Insurance.Queries.CalculateProductInsurance
{
    public class CalculateOrderInsuranceQuery : IRequest<InsuranceViewModel>
    {
        public List<int> ProductIds { get; set; }
    }

    public class CalculateOrderInsuranceQueryHandler : IRequestHandler<CalculateOrderInsuranceQuery, InsuranceViewModel>
    {
        private readonly IInsuranceService _insuranceService;

        public CalculateOrderInsuranceQueryHandler(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        public async Task<InsuranceViewModel> Handle(CalculateOrderInsuranceQuery request, CancellationToken cancellationToken)
        {
            var insuranceValue = await _insuranceService.CalculateBatchInsurance(request.ProductIds, cancellationToken);

            var model = new InsuranceViewModel
            {
                InsuranceValue = insuranceValue,
            };

            return await Task.FromResult(model);

        }
    }
}
