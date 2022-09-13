using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;

namespace Shopping.Aggregator.Controllers.V1
{
    [ApiVersion("1.0")]
    public class ShoppingController : BaseController
    {
        private readonly ICatalogService _catalog;
        private readonly IBasketService _basket;
        private readonly IOrderService _order;

        public ShoppingController(ICatalogService catalog, IBasketService basket, IOrderService order)
        {
            _catalog = catalog;
            _basket = basket;
            _order = order;
        }
        [HttpGet("{userName}" , Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel) , (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            var basket = await _basket.GetBasket(userName);
            foreach (var item in basket.Items)
            {
                var product = await _catalog.GetCatalog(item.ProductId);


                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                product.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }

            var orders = await _order.GetOrderByUserName(userName);
            var shoppingModel = new ShoppingModel()
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };

            return Ok(shoppingModel);
        }
    }
}
