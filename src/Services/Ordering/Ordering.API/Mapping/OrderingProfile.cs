﻿using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Order.Commands.CheckoutOrder;

namespace Ordering.API.Mapping
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckOutEvent>().ReverseMap();

        }
    }
}
