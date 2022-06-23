using Basket.API.Common.Dependency;
using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService // not working: IScopedDependency
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _client;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CouponModel> GetDiscount(string productname)
        {
            var discountrequest = new GetDiscountRequest {ProductName = productname};
            // call grpc and requset
            return await _client.GetDiscountAsync(discountrequest);
        }
    }
}
