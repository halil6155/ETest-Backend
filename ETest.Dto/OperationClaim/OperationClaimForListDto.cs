using Core.Entities.Abstract;

namespace ETest.Dto.OperationClaim
{
    public class OperationClaimForListDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}