using AutoMapper;
using Carrefour.Management.Application.OrderApplication.Models.Dto;
using Carrefour.Management.Repository.Entities;

namespace Carrefour.Management.Application
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Order, NewOrderDTO>().ReverseMap();
        }
    }
}
