using MediatR;

namespace CoolBlue.Products.Application.Common.Interfaces
{
    public interface IInsuranceService
    {
        Task<float> CalculateInsurance(int productId, CancellationToken cancellationToken);
        Task<float> CalculateBatchInsurance(List<int> productIdList, CancellationToken cancellationToken);
        Task AddSurcharge(int productTypeId, double surchargeRate, CancellationToken cancellationToken);
    }
}
