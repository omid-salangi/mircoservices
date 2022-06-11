using Discount.Grpc.Common.Profiles;

namespace Discount.Grpc.Webframework.Configuration
{
    public static class AutoMapperConfiguration
    {
        
        public static void AddAutoMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

        }


    }
}
