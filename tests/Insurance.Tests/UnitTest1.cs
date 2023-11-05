//using System;
//using CoolBlue.Products.Application.Insurance.Queries.CalculateInsurance;
//using Insurance.Api.Controllers;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing;
//using Microsoft.Extensions.Hosting;
//using Newtonsoft.Json;
//using Xunit;

//namespace Insurance.Tests
//{
//    public class InsuranceTests : IClassFixture<ControllerTestFixture>
//    {
//        private readonly ControllerTestFixture _fixture;

//        public InsuranceTests(ControllerTestFixture fixture)
//        {
//            _fixture = fixture;
//        }

//        [Fact]
//        public void CalculateInsurance_GivenSalesPriceBetween500And2000Euros_ShouldAddThousandEurosToInsuranceCost()
//        {
//            const float expectedInsuranceValue = 1000;

//            var dto = new CalculateInsuranceQuery
//            {
//                ProductId = 1,
//            };
//            var sut = new InsuranceController();

//            var result = sut.CalculateInsurance(dto);

//            Assert.Equal(
//                expected: expectedInsuranceValue,
//                actual: result.InsuranceValue
//            );
//        }

//        [Fact]
//        public void CalculateInsurance_GivenProductIsLaptop_GivenSalesLessThan500_ShouldAdd500EurosToInsuranceCost()
//        {
//            const float expectedInsuranceValue = 500;

//            var dto = new InsuranceController.InsuranceDto
//            {
//                ProductId = 2,
//                ProductTypeName = "Laptops",
//                ProductTypeHasInsurance = true,
//            };
//            var sut = new InsuranceController();

//            var result = sut.CalculateInsurance(dto);

//            Assert.Equal(
//                expected: expectedInsuranceValue,
//                actual: result.InsuranceValue
//            );
//        }
//    }

//    public class ControllerTestFixture : IDisposable
//    {
//        private readonly IHost _host;

//        public ControllerTestFixture()
//        {
//            _host = new HostBuilder()
//                   .ConfigureWebHostDefaults(
//                        b => b.UseUrls("http://localhost:5002")
//                              .UseStartup<ControllerTestStartup>()
//                    )
//                   .Build();

//            _host.Start();
//        }

//        public void Dispose() => _host.Dispose();
//    }

//    public class ControllerTestStartup
//    {
//        public void Configure(IApplicationBuilder app)
//        {
//            app.UseRouting();
//            app.UseEndpoints(
//                ep =>
//                {
//                    ep.MapGet(
//                        "products/{id:int}",
//                        context =>
//                        {
//                            int productId = int.Parse((string)context.Request.RouteValues["id"]);
//                            var product = new
//                            {
//                                id = productId,
//                                name = "Test Product",
//                                productTypeId = 1,
//                                salesPrice = 750
//                            };
//                            return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
//                        }
//                    );
//                    ep.MapGet(
//                        "product_types",
//                        context =>
//                        {
//                            var productTypes = new[]
//                                               {
//                                                   new
//                                                   {
//                                                       id = 1,
//                                                       name = "Test type",
//                                                       canBeInsured = true
//                                                   },
//                                                    new
//                                                   {
//                                                       id = 2,
//                                                       name = "Laptops",
//                                                       canBeInsured = true
//                                                   },
//                                               };
//                            return context.Response.WriteAsync(JsonConvert.SerializeObject(productTypes));
//                        }
//                    );
//                }
//            );
//        }
//    }
//}