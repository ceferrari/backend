using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Backend.Application.Results;
using Backend.Application.Services;
using Backend.Domain.ViewModels.CartViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute]Guid id, [FromQuery]string customerId)
        {
            if (id != Guid.Empty && customerId == null)
            {
                return await _cartService.GetById(id);
            }

            if (id == Guid.Empty && customerId != null)
            {
                return await _cartService.GetByCustomerId(customerId);
            }

            return await _cartService.GetAll();
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CartViewModel cart)
        {
            return await _cartService.Create(cart);
        }

        [HttpPatch("{id}/items")]
        public async Task<IActionResult> Patch([FromRoute]Guid id, [FromBody]CartItemViewModel item)
        {
            return await _cartService.Update(id, item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await _cartService.Delete(id);
        }

        [HttpDelete("{id}/items/{item_id}")]
        public async Task<IActionResult> DeleteItem([FromRoute]Guid id, [FromRoute]Guid itemId)
        {
            return await _cartService.DeleteItem(id, itemId);
        }

        [HttpPost("{id}/checkout")]
        public async Task<IActionResult> Checkout([FromRoute]Guid id, [FromBody]CartCheckoutViewModel checkout)
        {
            var xTeamControl = Request.Headers["x-team-control"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(xTeamControl))
            {
                return new FailureActionResult(HttpStatusCode.BadRequest, "'x-team-control' header is missing");
            }

            return await _cartService.Checkout(id, checkout.CurrencyCode, xTeamControl);
        }
    }
}
