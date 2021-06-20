using Core.Entities.Abstract;

namespace ETest.Dto.UserOperationClaim
{
    public class UserOperationClaimForAddDto:IDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int OperationClaimId { get; set; }
        public string OperationClaimName { get; set; }
        public bool IsCheck { get; set; }
    }
}