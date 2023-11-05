using CoolBlue.Products.Application.Common.Interfaces;

namespace CoolBlue.Products.Application.Common.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IProductDataIntegrationServices _productDataIntegration;

        public InsuranceService(IProductDataIntegrationServices productDataIntegration)
        {
            _productDataIntegration = productDataIntegration;
        }

        public async Task<float> CalculateInsurance(int productId, CancellationToken cancellationToken)
        {
            var list = new List<int> { productId };
            return await CalculateBatchInsurance(list, cancellationToken);
        }

        public async Task<float> CalculateBatchInsurance(List<int> productIdList, CancellationToken cancellationToken)
        {
            float insuranceValue = 0f;

            foreach (var productId in productIdList)
            {
                var productType = await _productDataIntegration.GetProductTypeByProductAsync(productId, cancellationToken);
                var salesPrice = await _productDataIntegration.GetSalesPriceAsync(productId, cancellationToken);


                if (productType.HasInsurance)
                {
                    if (productType.Name == "Laptops" || productType.Name == "Smartphones")
                        insuranceValue += 500;
                    if (salesPrice > 500 && salesPrice < 2000)
                        insuranceValue += 1000;
                    else if (salesPrice >= 2000)
                        insuranceValue += 2000;
                }
            }

            return insuranceValue;
        }
    }
}
