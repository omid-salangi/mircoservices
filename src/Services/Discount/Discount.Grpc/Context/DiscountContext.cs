using Discount.Grpc.Common.Dependency;
using Discount.Grpc.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Context
{
    public class DiscountContext  : DbContext , IScopedDependency
    {
        public DiscountContext(DbContextOptions options) : base(options)
        {
           
        }

        public DbSet<Coupon> Coupons { get; set; }
  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            base.OnModelCreating(modelBuilder);
        }
    }
}
