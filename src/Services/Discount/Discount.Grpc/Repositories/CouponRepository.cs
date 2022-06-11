using Discount.Grpc.Common.Dependency;
using Discount.Grpc.Context;
using Discount.Grpc.Entities;
using Discount.Grpc.Interface;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Repositories
{
    public class CouponRepository : ICouponRepository , IScopedDependency
    {
        private readonly DiscountContext _context;

        public CouponRepository(DiscountContext context)
        {
            _context = context;
        }

        public async Task<Coupon> GetCoupon(string productname)
        {
            return await _context.Coupons.Where(c => c.ProductName == productname).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateCoupon(Coupon coupon)
        {
            var temp = new Coupon()
            {
                Description = coupon.Description ,
                Amount = coupon.Amount,
                ProductName = coupon.ProductName
            };
            await _context.Coupons.AddAsync(temp);
            try
            {
                var res = await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
                
            }

            return true;

        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            var temp = await _context.Coupons.Where(c => c.ProductName == coupon.ProductName).FirstOrDefaultAsync();
            if (temp == null)
            {
                return false;   
            }

            temp.ProductName = coupon.ProductName;
            temp.Amount = coupon.Amount;
            temp.Description = coupon.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteCoupon(string productname)
        {
            var temp = await _context.Coupons.Where(c => c.ProductName == productname).FirstOrDefaultAsync();
            if (temp == null)
            {
                return false;
            }

            try
            {
                _context.Coupons.Remove(temp);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
