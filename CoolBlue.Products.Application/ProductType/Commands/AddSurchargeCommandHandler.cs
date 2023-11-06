using CoolBlue.Products.Application.Common.Interfaces;
using MediatR;

namespace CoolBlue.Products.Application.ProductType.Commands
{
    public class AddSurchargeCommand : IRequest
    {
        public double SurchargeRate { get; set; }
        public int ProductTypeId { get; set; }
    }

    public class AddSurchargeCommandHandler : IRequestHandler<AddSurchargeCommand>
    {
        private readonly IInsuranceService _insuranceService;

        public AddSurchargeCommandHandler(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        public async Task Handle(AddSurchargeCommand request, CancellationToken cancellationToken)
        {
            await _insuranceService.AddSurcharge(request.ProductTypeId, request.SurchargeRate, cancellationToken);            
            return;
        }
    }
}
