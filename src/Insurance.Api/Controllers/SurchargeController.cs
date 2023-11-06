using CoolBlue.Products.Application.ProductType.Commands;
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
        /// Add surcharge to product type
        /// </summary>
        /// <param name="command">Surcharge command including productTypeId and surchargeRate</param>
        /// <returns>Returns an actionresult</returns>
        [HttpPost]
        [Route("product")]
        public async Task<ActionResult> AddSurcharge([FromBody] AddSurchargeCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

    }
}