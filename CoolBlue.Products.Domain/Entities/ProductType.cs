using System.ComponentModel.DataAnnotations;

namespace CoolBlue.Products.Domain.Entities
{
    public class ProductType : BaseEntity
    {
        public bool HasInsurance { get; set; }
        [ConcurrencyCheck]
        public double SurchargeRate { get; set; }

    }
}
