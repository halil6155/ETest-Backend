using AutoMapper;
using ETest.Dto.OperationClaim;
using ETest.Entities.Concrete;

namespace ETest.Business.Mapping.AutoMapper.Profiles
{
    public class OperationClaimProfile:Profile
    {
        public OperationClaimProfile()
        {
            CreateMap<OperationClaimForAddDto, OperationClaim>();
            CreateMap<OperationClaimForUpdateDto, OperationClaim>();
            CreateMap<OperationClaim, OperationClaimForListDto>();
            CreateMap<OperationClaim, OperationClaimForSingleDto>();
        }
    }
}