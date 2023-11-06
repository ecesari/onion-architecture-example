namespace CoolBlue.Products.Domain.Entities
{
    public class ProductType : BaseEntity
    {
        public bool HasInsurance { get; set; }
        public double SurchargeRate { get; set; }

    }
}
