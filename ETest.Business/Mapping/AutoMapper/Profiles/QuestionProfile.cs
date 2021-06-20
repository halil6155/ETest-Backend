using AutoMapper;
using ETest.Dto.Question;
using ETest.Entities.Concrete;

namespace ETest.Business.Mapping.AutoMapper.Profiles
{
    public class QuestionProfile:Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionForAddDto, Question>();
            CreateMap<QuestionForUpdateDto, Question>();
            CreateMap<Question, QuestionForListDto>();
            CreateMap<Question, QuestionForSingleDto>();
        }
    }
}