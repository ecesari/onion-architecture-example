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
            var productTypeList = _productDataIntegration.GetProductTypeAsync(productId);
            var salesPrice = _productDataIntegration.GetSalesPriceAsync(productId);

            float insuranceValue = 0f;

            if (productTypeList.Any(x => x.HasInsurance))
            {
                if (productTypeList.Any(x => x.Name == "Laptops") || productTypeList.Any(x => x.Name == "Smartphones"))
                    insuranceValue += 500;
                if (salesPrice > 500 && salesPrice < 2000)
                    insuranceValue += 1000;
                else if (salesPrice >= 2000)
                    insuranceValue += 2000;
            }

            //TODO: figure out if product type is a list
            var model = new InsuranceViewModel
            {
                InsuranceValue = insuranceValue,
                ProductId = productId,
                ProductTypeHasInsurance = productTypeList.FirstOrDefault().HasInsurance,
                SalesPrice = salesPrice,
                ProductTypeName = productTypeList.FirstOrDefault().Name,
            };

            return await Task.FromResult(model);

        }


    }
}
