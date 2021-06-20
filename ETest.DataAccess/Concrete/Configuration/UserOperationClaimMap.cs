using ETest.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETest.DataAccess.Concrete.Configuration
{
    public class UserOperationClaimMap:IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.HasKey(x => new { x.UserId, x.OperationClaimId });
            builder.HasIndex(x => new { x.UserId, x.OperationClaimId });
            builder.ToTable("UserOperationClaims");
        }
    }
}