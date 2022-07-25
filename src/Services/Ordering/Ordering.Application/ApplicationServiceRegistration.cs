using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviour;
using Ordering.Application.Behaviours;
using System.Reflection;

namespace Ordering.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServivces(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly()); //profile
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // abstractvalidator
            services.AddMediatR(Assembly.GetExecutingAssembly()); // getexecutingassembly means it will search for related files // irequest and irequesthandler 
          
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
