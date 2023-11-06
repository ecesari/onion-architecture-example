﻿using CoolBlue.Products.Application.Common.Constants;
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

        public async Task<double> CalculateInsurance(int productId, CancellationToken cancellationToken)
        {
            var list = new List<int> { productId };
            return await CalculateBatchInsurance(list, cancellationToken);
        }

        public async Task<double> CalculateBatchInsurance(List<int> productIdList, CancellationToken cancellationToken)
        {
            int cameraCount = 0;
            double insuranceValue = 0f;

            foreach (var productId in productIdList)
            {
                var productTypeFromIntegration = await _productDataIntegration.GetProductTypeByProductAsync(productId, cancellationToken);
                var productTypeDb = await _productTypeRepository.GetByIdAsync(productTypeFromIntegration.Id);
                if (productTypeDb != null)
                    insuranceValue += productTypeDb.SurchargeRate;
                var salesPrice = await _productDataIntegration.GetSalesPriceAsync(productId, cancellationToken);


                if (productTypeFromIntegration.HasInsurance)
                {
                    if (productTypeFromIntegration.Name == ProductTypeConstants.DigitalCamera)
                        cameraCount++;
                    if (productTypeFromIntegration.Name == ProductTypeConstants.Laptops || productTypeFromIntegration.Name == ProductTypeConstants.Smartphones)
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
