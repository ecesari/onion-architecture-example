using CoolBlue.Products.Application.Common;
using CoolBlue.Products.Application.Insurance.Models;
using MediatR;

namespace CoolBlue.Products.Application.Insurance.Queries.CalculateInsurance
{
    public class CalculateInsuranceQuery : IRequest<InsuranceViewModel>
    {
        public int ProductId { get; set; }
    }

    public class CalculateInsuranceQueryHandler : IRequestHandler<CalculateInsuranceQuery, InsuranceViewModel>
    {
        private readonly IProductDataIntegration _productDataIntegration;

        public CalculateInsuranceQueryHandler(IProductDataIntegration productDataIntegration)
        {
            _productDataIntegration = productDataIntegration;
        }

        public async Task<InsuranceViewModel> Handle(CalculateInsuranceQuery request, CancellationToken cancellationToken)
        {
            int productId = request.ProductId;
            var productType = await _productDataIntegration.GetProductTypeByProductAsync(productId, cancellationToken);
            var salesPrice = await _productDataIntegration.GetSalesPriceAsync(productId, cancellationToken);

            float insuranceValue = 0f;

            if (productType.HasInsurance)
            {
                if (productType.Name == "Laptops" || productType.Name == "Smartphones")
                    insuranceValue += 500;
                if (salesPrice > 500 && salesPrice < 2000)
                    insuranceValue += 1000;
                else if (salesPrice >= 2000)
                    insuranceValue += 2000;
            }

            var model = new InsuranceViewModel
            {
                InsuranceValue = insuranceValue,
                ProductId = productId,
                ProductTypeHasInsurance = productType.HasInsurance,
                SalesPrice = salesPrice,
                ProductTypeName = productType.Name,
            };

            return await Task.FromResult(model);

        }


    }
}
