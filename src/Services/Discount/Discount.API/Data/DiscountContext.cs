using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discount.API.Common.Dependency;
using Discount.API.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Discount.API.Data
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
