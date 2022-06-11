using Discount.Grpc.Entities;

namespace Discount.Grpc.Interface
{
    public interface ICouponRepository
    {
        Task<Coupon> GetCoupon(string productname);
        Task<bool> CreateCoupon(Coupon coupon);
        Task<bool> UpdateCoupon(Coupon coupon);
        Task<bool> DeleteCoupon(string productname);
    }
}
 