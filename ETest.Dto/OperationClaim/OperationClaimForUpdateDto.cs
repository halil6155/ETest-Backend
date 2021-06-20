using Core.Entities.Abstract;

namespace ETest.Dto.OperationClaim
{
    public class OperationClaimForUpdateDto:IDto
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}