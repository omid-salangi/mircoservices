using AutoMapper;
using Ordering.Application.Features.Order.Commands.CheckoutOrder;
using Ordering.Application.Features.Order.Commands.UpdateOrder;
using Ordering.Application.Features.Order.Queries.GetOrdersList;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mapping
{
    public class MappingProfile : Profile // for automapper 
    {
        public MappingProfile()
        {
            CreateMap<Order, OrdersVm>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();  // update 
        }
    }
}