﻿using Discount.API.Data.Model;
using Discount.API.ViewModels;

namespace Discount.API.Data.Interface
{
    public interface ICouponRepository
    {
        Task<Coupon> GetCoupon(string productname);
        Task<bool> CreateCoupon(Coupon coupon);
        Task<bool> UpdateCoupon(Coupon coupon);
        Task<bool> DeleteCoupon(string productname);
    }
}
 