using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Utilities.Security.Hashing;
using ETest.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ETest.DataAccess.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void CustomSeedDate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperationClaim>().HasData(new List<OperationClaim>
            {
                new OperationClaim {Name = "Admin", Id = 1},
                new OperationClaim{Name = "Default",Id = 2}
            });
            HashingHelper.CreatePasswordHash("12345",out var passwordHash,out var passwordSalt);
            modelBuilder.Entity<User>().HasData(new List<User>
            {
                new User
                {
                    CreatedOn = DateTime.Now, DateOfBirth = DateTime.Now, Email = "user@mail.com", IsActive = true,
                    Id = 1,
                    PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                    FirstName = "Örnek",
                    LastName = "Kullanıcı",
                    ImageUrl = "default-user.jpeg",
                    UserName = "ornek.admin",
                }
            });
            modelBuilder.Entity<UserOperationClaim>().HasData(new List<UserOperationClaim>
            {
                new UserOperationClaim
                {
                    OperationClaimId = 1,
                    UserId = 1
                }
            });
        }
        public static ModelBuilder MapConfiguration(this ModelBuilder modelBuilder)
        {
            return modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public static ModelBuilder CascadeConfiguration(this ModelBuilder modelBuilder)
        {
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()).Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            return modelBuilder;
        }
    }
}