using Core.Entities.Abstract;

namespace ETest.Dto.UserAnswer
{
    public class UserAnswerForResultDto:IDto
    {
        public int TotalQuestion { get; set; }
        public int CorrectNumber { get; set; }
        public int WrongNumber { get; set; }
    }
}