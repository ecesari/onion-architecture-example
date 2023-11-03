using CoolBlue.Products.Application.Common;
using CoolBlue.Products.Application.Insurance.Queries.CalculateInsurance;
using CoolBlue.Products.Application.ProductType.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBlue.Products.Infrastructure.Integration
{
    internal class ProductDataIntegration : IProductDataIntegration
    {
        private const string ProductApi = "http://localhost:5002";

        public List<ProductTypeViewModel> GetProductTypeAsync(int productId)
        {

            HttpClient client = new HttpClient { BaseAddress = new Uri(ProductApi) };
            string json = client.GetAsync("/product_types").Result.Content.ReadAsStringAsync().Result;
            var collection = JsonConvert.DeserializeObject<dynamic>(json);

            json = client.GetAsync(string.Format("/products/{0:G}", productId)).Result.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<dynamic>(json);

            int productTypeId = product.productTypeId;
            string productTypeName = null;
            bool hasInsurance = false;

    

            var returnModel = new List<ProductTypeViewModel>();
            for (int i = 0; i < collection.Count; i++)
            {
                if (collection[i].id == productTypeId && collection[i].canBeInsured == true)
                {
                    returnModel.Add(new ProductTypeViewModel
                    {
                        Name = collection[i].name,
                        HasInsurance = true,
                    });
                }
            }

            return returnModel;
        }



        public int GetSalesPriceAsync(int productId)
        {
            HttpClient client = new HttpClient { BaseAddress = new Uri(ProductApi) };
            string json = client.GetAsync(string.Format("/products/{0:G}", productId)).Result.Content.ReadAsStringAsync().Result;
            var product = JsonConvert.DeserializeObject<dynamic>(json);

            return product.salesPrice;
        }
    }
}
