using AutoMapper;
using ETest.Dto.Category;
using ETest.Entities.Concrete;

namespace ETest.Business.Mapping.AutoMapper.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryForAddDto, Category>();
            CreateMap<CategoryForUpdateDto, Category>();
            CreateMap<Category, CategoryForListDto>();
            CreateMap<Category, CategoryForSingleDto>();
        }
    }
}