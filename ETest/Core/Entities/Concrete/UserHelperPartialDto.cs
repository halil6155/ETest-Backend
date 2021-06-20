using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{
    public class UserHelperPartialDto:IDto
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public string[] Roles { get; set; }
    }
}