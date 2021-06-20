using Core.Entities.Abstract;

namespace ETest.Dto.OperationClaim
{
    public class OperationClaimForSingleDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}