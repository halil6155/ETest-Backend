using System;
using Core.Entities.Abstract;

namespace ETest.Dto.Question
{
    public class QuestionForListDto:IDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public char Answer { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string UserName { get; set; }
    }
}