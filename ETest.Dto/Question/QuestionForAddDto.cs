using Core.Entities.Abstract;

namespace ETest.Dto.Question
{
    public class QuestionForAddDto:IDto
    {
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public char Answer { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}