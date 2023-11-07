using CoolBlue.Products.Application.Common.Interfaces;
using CoolBlue.Products.Application.Insurance.Models;
using MediatR;

namespace CoolBlue.Products.Application.Insurance.Queries.CalculateProductInsurance
{
    public class CalculateProductInsuranceQuery : IRequest<InsuranceViewModel>
    {
        public int ProductId { get; set; }
    }

    public class CalculateProductInsuranceQueryHandler : IRequestHandler<CalculateProductInsuranceQuery, InsuranceViewModel>
    {
        private readonly IInsuranceService _insuranceService;

        public CalculateProductInsuranceQueryHandler(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        public async Task<InsuranceViewModel> Handle(CalculateProductInsuranceQuery request, CancellationToken cancellationToken)
        {
            var insuranceValue = await _insuranceService.CalculateInsurance(request.ProductId, cancellationToken);
            var returnModel = new InsuranceViewModel { InsuranceValue = insuranceValue };
            return await Task.FromResult(returnModel);

        }
    }
}