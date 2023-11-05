using Coolblue.Products.Tests.Common.Models;
using Newtonsoft.Json;
using System.Reflection;
using Xunit.Sdk;

namespace Coolblue.Products.Tests.Common.Attributes
{
    public class OrderModelDataAttribute : DataAttribute
    {
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            string sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string sFile = System.IO.Path.Combine(sCurrentDirectory, @"../../../TestData/ProductList.json");
            string sFilePath = Path.GetFullPath(sFile);

            var json = File.ReadAllText(sFilePath);

            var testData = JsonConvert.DeserializeObject<OrderTestModel>(json);
            var objectArray = new[] { testData };
            var list = new List<object[]>();
            list.Add(objectArray);

            return list;
        }
    }
}