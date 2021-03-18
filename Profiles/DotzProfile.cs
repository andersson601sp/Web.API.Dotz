using AutoMapper;
using Web.API.Dotz.Dtos;
using Web.API.Dotz.Entities;
using Web.API.Dotz.Models;

namespace Web.API.Dotz.Profiles
{
    public class DotzProfile : Profile
    {
        public DotzProfile()
        {
            CreateMap<UserDto, User>();

            CreateMap<User, UserDto>()
              .ForMember(
                  dest => dest.Password,
                  opt => opt.MapFrom(src => "******")
              );

            CreateMap<User, UserAuthDto>();
            CreateMap<UserAddress, UserAddressDto>().ReverseMap();
            CreateMap<Product, ProductModel>();
            CreateMap<UserDotz, UserDotzModel>();
            CreateMap<Order, OrderModel>().ReverseMap();
             /*.ForMember(
                 dest => dest.OrderItems,
                 opt => opt.MapFrom(src => src.OrderItems)
             );
             CreateMap<OrderModel, Order>().ReverseMap();*/
            CreateMap<OrderItems, OrderItemsModel>().ReverseMap();


        }
    }
}