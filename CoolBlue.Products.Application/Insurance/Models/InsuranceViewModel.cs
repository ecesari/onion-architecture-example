using System.Text.Json.Serialization;

namespace CoolBlue.Products.Application.Insurance.Models
{
    public class InsuranceViewModel
    {
        public int ProductId { get; set; }
        public float InsuranceValue { get; set; }
        [JsonIgnore]
        public string ProductTypeName { get; set; }
        [JsonIgnore]
        public bool ProductTypeHasInsurance { get; set; }
        [JsonIgnore]
        public float SalesPrice { get; set; }
    }
}
