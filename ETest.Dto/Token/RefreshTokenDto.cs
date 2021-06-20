using Core.Entities.Abstract;

namespace ETest.Dto.Token
{
    public class RefreshTokenDto:IDto
    {
        public string RefreshToken { get; set; }
    }
}