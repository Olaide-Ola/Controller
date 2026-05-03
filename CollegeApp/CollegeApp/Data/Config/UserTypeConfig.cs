using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class UserTypeConfig : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.ToTable("UserTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Description).HasMaxLength(1550);

            builder.HasData(new List<UserType>()
            {
                new UserType { Id = 1, Name = "Student", Description = "For User type Student" },
                new UserType { Id = 2, Name = "Faculty", Description = "For User type Faculty" },
                new UserType { Id = 3, Name = "Suppoting Staff", Description = "For User type Supporting Staff" },
                new UserType { Id = 4, Name = "Parent", Description = "For User type Parent" }
            });
        }
    }
}
