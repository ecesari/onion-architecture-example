namespace Coolblue.Products.Tests.Common.Models
{
    public class OrderTestModel
    {
        public double InsuranceTotal { get; set; }
        public List<OrderLineTestModel> OrderLines { get; set; }
    }
}