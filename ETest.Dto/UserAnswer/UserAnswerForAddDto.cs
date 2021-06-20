using Core.Entities.Abstract;

namespace ETest.Dto.UserAnswer
{
    public class UserAnswerForAddDto:IDto
    {
        public char Answer { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
    }
}