using CoolBlue.Products.Application.Insurance.Models;
using CoolBlue.Products.Application.Insurance.Queries.CalculateInsurance;
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

        public InsuranceController()
        {
        }

        public InsuranceController(IMediator mediator)
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
        public async Task<ActionResult<InsuranceViewModel>> CalculateInsurance([FromBody] CalculateInsuranceQuery query)
        {
            return await _mediator.Send(query);
        }



    }
}