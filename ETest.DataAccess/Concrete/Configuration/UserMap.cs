using ETest.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETest.DataAccess.Concrete.Configuration
{
    public class UserMap:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.FirstName).HasMaxLength(25);
            builder.Property(x => x.LastName).HasMaxLength(25);
            builder.Property(x => x.UserName).HasMaxLength(25);
            builder.Property(x => x.Email).HasMaxLength(50);
            builder.Property(x => x.RefreshToken).HasMaxLength(120);
            builder.Property(x => x.ImageUrl).HasMaxLength(350);
            builder.ToTable("Users");
        }
    }
}