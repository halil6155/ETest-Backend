using ETest.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETest.DataAccess.Concrete.Configuration
{
    public class QuestionMap:IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Text).HasMaxLength(500).IsRequired();
            builder.Property(x => x.OptionA).HasMaxLength(100).IsRequired();
            builder.Property(x => x.OptionB).HasMaxLength(100).IsRequired();
            builder.Property(x => x.OptionC).HasMaxLength(100).IsRequired();
            builder.Property(x => x.OptionD).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Answer).IsRequired();
     
            builder.ToTable("Questions");
        }
    }
}