using AutoMapper;
using EShop.Core.Dtos;
using EShop.Core.Entities;

namespace EShop.Helpers
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<AppUser, UserDto>();
      CreateMap<UserDto, AppUser>();
    }
  }
}
