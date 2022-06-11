using Discount.Grpc.Interface;
using Discount.Grpc.Protos;
using Grpc.Core;
using AutoMapper;
using Discount.Grpc.Entities;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ICouponRepository _repository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;
        
        public DiscountService(ICouponRepository repository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetCoupon(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,
                    $"Discount with ProductName={request.ProductName} is not found."));
            }

            var couponModel = _mapper.Map<CouponModel>(coupon); // add to program 
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _repository.CreateCoupon(coupon);
           
           _logger.LogInformation("Discount is successfully created. Product name : {ProductName}" , coupon.ProductName);
           return _mapper.Map<CouponModel>(coupon);

        }

        public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _repository.UpdateCoupon(coupon);

            _logger.LogInformation("Discount is successfully updated. Product name : {ProductName}", coupon.ProductName);
            return _mapper.Map<CouponModel>(coupon);
        }

        public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var res =await _repository.DeleteCoupon(request.ProductName);
            var re = new DeleteDiscountResponse()
            {
                Succes = res
            };
            return re;
        }
    }
}
