using Core.Entities.Abstract;

namespace ETest.Dto.Category
{
    public class CategoryForUpdateDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}