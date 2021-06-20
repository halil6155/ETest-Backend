using System.Collections.Generic;
using Core.Entities.Abstract;
using ETest.Entities.Concrete;

namespace ETest.Entities.Models
{
    public class UserOperationClaimModel:IModel
    {
        public User User { get; set; }
        public List<OperationClaim> OperationClaims { get; set; }
    }
}