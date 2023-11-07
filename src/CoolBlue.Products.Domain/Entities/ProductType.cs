using System.ComponentModel.DataAnnotations;

namespace CoolBlue.Products.Domain.Entities
{
    public class ProductType : BaseEntity
    {
        [ConcurrencyCheck]
        public double SurchargeRate { get; set; }

    }
}
