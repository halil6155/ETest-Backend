using Core.Entities.Abstract;

namespace ETest.Dto.Question
{
    public class QuestionForFilterDto:IDto
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}