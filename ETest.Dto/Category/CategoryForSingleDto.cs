using System;
using Core.Entities.Abstract;

namespace ETest.Dto.Category
{
    public class CategoryForSingleDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
    }
}