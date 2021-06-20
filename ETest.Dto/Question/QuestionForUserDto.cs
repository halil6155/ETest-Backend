using Core.Entities.Abstract;

namespace ETest.Dto.Question
{
    public class QuestionForUserDto:IDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public int CategoryId { get; set; }
        public char Answer { get; set; }
        public int Total { get; set; }
        public int CorrectNumber { get; set; }
        public int WrongNumber { get; set; }
    }
}