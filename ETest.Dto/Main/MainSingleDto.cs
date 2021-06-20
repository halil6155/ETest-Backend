using Core.Entities.Abstract;

namespace ETest.Dto.Main
{
    public class MainSingleDto:IDto
    {
        public int TotalQuestion { get; set; }
        public int TotalCategory { get; set; }
        public int ActiveUser { get; set; }
        public int DisabledUser { get; set; }

    }
}