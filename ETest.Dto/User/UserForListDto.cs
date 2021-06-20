using System;
using Core.Entities.Abstract;

namespace ETest.Dto.User
{
    public class UserForListDto:IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}