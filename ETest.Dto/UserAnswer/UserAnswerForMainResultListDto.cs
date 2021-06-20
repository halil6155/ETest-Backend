using Core.Entities.Abstract;

namespace ETest.Dto.UserAnswer
{
    public class UserAnswerForMainResultListDto:IDto
    {
        public string CategoryName { get; set; }
        public int TotalQuestion { get; set; }
        public int CorrectNumber { get; set; }
        public int WrongNumber { get; set; }
    }
}