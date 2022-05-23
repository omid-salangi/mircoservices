using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers.V1._0
{
    [ApiVersion("1.0")]
    public class Basketcontroller : BaseController
    {
        private readonly IBasketRepository _repository;

        public Basketcontroller(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(_repository));
        }
        [HttpGet("{username}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart) , (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
        {
            var basket = await _repository.GetBasket(username);
            return Ok(basket ?? new ShoppingCart(username));
        }
        [HttpPost("UpdateBasket")]
        [ProducesResponseType(typeof(ShoppingCart) , (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            return Ok(_repository.UpdateBasket(basket));
        }

        [HttpDelete("{username}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string username)
        {
            await _repository.DeleteBasket(username);
            return Ok();
        }
    }
}
