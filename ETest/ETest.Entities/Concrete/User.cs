using System;
using System.Collections.Generic;
using Core.Entities.Abstract;

namespace ETest.Entities.Concrete
{
    public class User:IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string RefreshToken { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}