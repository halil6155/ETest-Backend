using Core.Entities.Abstract;

namespace ETest.Dto.Category
{
    public class CategoryForAddDto:IDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}