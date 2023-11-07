using MediatR;

namespace CoolBlue.Products.Application.Common.Interfaces
{
    public interface IInsuranceService
    {
        Task<double> CalculateInsurance(int productId, CancellationToken cancellationToken);
        Task<double> CalculateBatchInsurance(List<int> productIdList, CancellationToken cancellationToken);
        Task AddSurcharge(int productTypeId, double surchargeRate, CancellationToken cancellationToken);
    }
}
