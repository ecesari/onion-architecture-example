using CoolBlue.Products.Application.Insurance.Models;
using CoolBlue.Products.Application.Insurance.Queries.CalculateProductInsurance;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : Controller
    {
        private readonly IMediator _mediator;

        public InsuranceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Calculate insurance
        /// </summary>
        /// <param name="productId">product Id</param>
        /// <returns>Returns an InsuranceViewModel</returns>
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<InsuranceViewModel>> CalculateInsurance(int productId)
        {
            return await _mediator.Send(new CalculateProductInsuranceQuery { ProductId = productId });
        }

        /// <summary>
        /// Calculate insurance
        /// </summary>
        /// <param name="query">Calculation query including productId list</param>
        /// <returns>Returns an InsuranceViewModel</returns>
        [HttpPost]
        [Route("order")]
        public async Task<ActionResult<InsuranceViewModel>> CalculateBatchInsurance([FromBody] CalculateOrderInsuranceQuery query)
        {
            return await _mediator.Send(query);
        }

    }
}