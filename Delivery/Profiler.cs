using AutoMapper;
using Delivery.DAL.Entities;
using Delivery.Models;
using System;

namespace Delivery
{
    public sealed class Profiler : Profile
    {
        public Profiler()
        {
            CreateMap<XMLDeliveryOrder, DeliveryOrderEntity>();
            CreateMap<DeliveryOrderEntity, DeliveryOrder>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(s => DateTime.Parse(s.Date)));
            CreateMap<DeliveryLogEntity, DeliveryLog>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(s => DateTime.Parse(s.Date)));
            CreateMap<DeliveryLogWithIPAddress, DeliveryLogEntity>();
        }
    }
}
