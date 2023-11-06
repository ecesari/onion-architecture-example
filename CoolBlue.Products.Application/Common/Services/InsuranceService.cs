using CoolBlue.Products.Application.Common.Interfaces;
using CoolBlue.Products.Domain.Repositories;

namespace CoolBlue.Products.Application.Common.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IProductDataIntegrationServices _productDataIntegration;
        private readonly IProductTypeRepository _productTypeRepository;

        public InsuranceService(IProductDataIntegrationServices productDataIntegration, IProductTypeRepository productTypeRepository)
        {
            _productDataIntegration = productDataIntegration;
            _productTypeRepository = productTypeRepository;
        }

        public async Task<float> CalculateInsurance(int productId, CancellationToken cancellationToken)
        {
            var list = new List<int> { productId };
            return await CalculateBatchInsurance(list, cancellationToken);
        }

        public async Task<float> CalculateBatchInsurance(List<int> productIdList, CancellationToken cancellationToken)
        {
            int cameraCount = 0;
            float insuranceValue = 0f;

            foreach (var productId in productIdList)
            {
                var productType = await _productDataIntegration.GetProductTypeByProductAsync(productId, cancellationToken);
                var salesPrice = await _productDataIntegration.GetSalesPriceAsync(productId, cancellationToken);


                if (productType.HasInsurance)
                {
                    if (productType.Name == "Digital cameras")
                        cameraCount++;
                    if (productType.Name == "Laptops" || productType.Name == "Smartphones")
                        insuranceValue += 500;
                    if (salesPrice > 500 && salesPrice < 2000)
                        insuranceValue += 1000;
                    else if (salesPrice >= 2000)
                        insuranceValue += 2000;
                }
            }
            if (cameraCount >= 1)
                insuranceValue += 500;

            return insuranceValue;
        }


        public async Task AddSurcharge(int productTypeId, double surchargeRate, CancellationToken cancellationToken)
        {
            var entity = await _productTypeRepository.GetByIdAsync(productTypeId);
            if (entity == null)
                throw new Exception("No product type was found!");
            entity.SurchargeRate = surchargeRate;
            await _productTypeRepository.UpdateAsync(entity);
            return;
        }
    }
}
