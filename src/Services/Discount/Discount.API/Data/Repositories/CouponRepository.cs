using Discount.API.Common.Dependency;
using Discount.API.Data.Interface;
using Discount.API.Data.Model;
using Discount.API.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Data.Repositories
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

        public async Task<bool> CreateCoupon(Coupondto coupon)
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

        public async Task<bool> UpdateCoupon(Coupondto coupon)
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
