using CoolBlue.Products.Domain.Repositories;
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
        private readonly IProductTypeRepository productTypeRep;
        public async Task Handle(AddSurchargeCommand request, CancellationToken cancellationToken)
        {
            var entity = await productTypeRep.GetByIdAsync(request.ProductTypeId);
            if (entity == null)
                throw new Exception("No product type was found!");
            entity.SurchargeRate = request.SurchargeRate;
            await productTypeRep.UpdateAsync(entity);
            return;
        }
    }
}
