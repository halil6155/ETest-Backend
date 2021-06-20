using AutoMapper;
using ETest.Dto.UserAnswer;
using ETest.Entities.Concrete;

namespace ETest.Business.Mapping.AutoMapper.Profiles
{
    public class UserAnswerProfile:Profile
    {
        public UserAnswerProfile()
        {
            CreateMap<UserAnswerForAddDto, UserAnswer>();
        }
    }
}