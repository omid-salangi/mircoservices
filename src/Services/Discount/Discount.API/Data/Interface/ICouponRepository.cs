using Discount.API.Data.Model;
using Discount.API.ViewModels;

namespace Discount.API.Data.Interface
{
    public interface ICouponRepository
    {
        Task<Coupon> GetCoupon(string productname);
        Task<bool> CreateCoupon(Coupondto coupon);
        Task<bool> UpdateCoupon(Coupondto coupon);
        Task<bool> DeleteCoupon(string productname);
    }
}
 