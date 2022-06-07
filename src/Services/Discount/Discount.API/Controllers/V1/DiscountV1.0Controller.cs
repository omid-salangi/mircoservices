using System.Net;
using Discount.API.Data.Interface;
using Discount.API.Data.Model;
using Discount.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers.V1
{
    [ApiVersion("1.0")]
    public class DiscountController : BaseController
    {
        private readonly ICouponRepository _coupon;

        public DiscountController(ICouponRepository coupon)
        {
            _coupon = coupon;
        }
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(Coupon),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productname)
        {
            var res = await _coupon.GetCoupon(productname);
            return Ok(res);
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(bool), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteDiscount(string productname)
        {
            var res = await _coupon.DeleteCoupon(productname);
            return Ok(res);
        }
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> UpdateDiscount(Coupondto coupon)

        {
            var res = await _coupon.UpdateCoupon(coupon);
            return Ok(res);
        }
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> CreateDiscount(Coupondto coupon)
        {
            var res = await _coupon.CreateCoupon(coupon);
            return Ok(res);
        }
    }
}