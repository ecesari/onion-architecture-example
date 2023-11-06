using CoolBlue.Products.Application.Insurance.Models;
using CoolBlue.Products.Application.Insurance.Queries.CalculateProductInsurance;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurchargeController : Controller
    {
        private readonly IMediator _mediator;

        public SurchargeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Calculate insurance
        /// </summary>
        /// <param name="query">Calculation query including productId</param>
        /// <returns>Returns an InsuranceViewModel</returns>
        [HttpPost]
        [Route("product")]
        public async Task<ActionResult<InsuranceViewModel>> AddSurcharge([FromBody] CalculateProductInsuranceQuery query)
        {
            return await _mediator.Send(query);
        }

    }
}