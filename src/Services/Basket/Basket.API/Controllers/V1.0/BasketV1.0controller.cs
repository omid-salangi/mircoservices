using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers.V1._0
{
    [ApiVersion("1.0")]
    public class Basketcontroller : BaseController
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discount;
        private readonly IPublishEndpoint _publish;
        private readonly IMapper _mapper;

        public Basketcontroller(IBasketRepository repository , DiscountGrpcService discount  , IMapper mapper,IPublishEndpoint publish)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(_repository));
            _discount = discount ?? throw new ArgumentNullException(nameof(_repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publish = publish ?? throw new ArgumentNullException(nameof(publish));
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
            // grpc
            foreach (var item in basket.Items)
            {
                var coupon = await _discount.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(_repository.UpdateBasket(basket));
        }

        [HttpDelete("{username}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int) HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBasket(string username)
        {
            await _repository.DeleteBasket(username);
            return Ok();
        }

        [HttpPost( "Checkout")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CheckOut([FromBody] BasketCheckOutEvent basketCheckOut)
        {
           
            var basket = await _repository.GetBasket(basketCheckOut.UserName);
            if (basket == null)
            {
                return BadRequest();
            }

            var eventMessage = _mapper.Map<BasketCheckOutEvent>(basketCheckOut);
            eventMessage.TotalPrice = basket.TotalPrice;
            await _publish.Publish(eventMessage);

            await _repository.DeleteBasket(basket.UserName);
            return Accepted();
        }
    }
}
