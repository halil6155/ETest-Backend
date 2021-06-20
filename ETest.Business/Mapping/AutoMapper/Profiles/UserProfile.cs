using AutoMapper;
using Core.Utilities.FormFiles;
using ETest.Dto.User;
using ETest.Entities.Concrete;

namespace ETest.Business.Mapping.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<User, UserForSingleDto>()
                .ForMember(dest => dest.ImageUrl,
                    opt => opt.MapFrom(i =>ImageHelper.BaseImageUrl+ i.ImageUrl));
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.IsActive,
                    opt => 
                        opt.MapFrom(i => i.IsActive ? "Aktif" : "Pasif"));
        }
    }
}