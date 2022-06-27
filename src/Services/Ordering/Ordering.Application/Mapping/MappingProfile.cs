using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Application.Features.Order.Commands.CheckoutOrder;
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
        }
    }
}