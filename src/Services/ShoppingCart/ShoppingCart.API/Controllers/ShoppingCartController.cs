using Microsoft.AspNetCore.Mvc;
using ShoppingCart.API.GrpcServices;
using ShoppingCart.API.Repositories;
using System.Net;
using System.Threading.Tasks;

namespace ShoppingCart.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _repository;
        private readonly DiscountGrpcService _discountGrpcService;

        public ShoppingCartController(IShoppingCartRepository repository, DiscountGrpcService discountGrpcService)
        {
            _repository = repository;
            _discountGrpcService = discountGrpcService;
        }

        [HttpGet("{userName}", Name = "GetShoppingCart")]
        [ProducesResponseType(typeof(Entities.ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.ShoppingCart>> GetShoppingCart(string userName)
        {
            var basket = await _repository.GetShoppingCart(userName);

            return Ok(basket ?? new Entities.ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Entities.ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.ShoppingCart>> UpdateShoppingCart([FromBody] Entities.ShoppingCart basket)
        {
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(await _repository.UpdateShoppingCart(basket));
        }

        [HttpDelete("{userName}", Name = "DeleteShoppingCart")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteShoppingCart(string userName)
        {
            await _repository.DeleteShoppingCart(userName);

            return Ok();
        }

        
    }
}