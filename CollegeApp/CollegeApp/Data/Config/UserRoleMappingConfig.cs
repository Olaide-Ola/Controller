using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class UserRoleMappingConfig : IEntityTypeConfiguration<UserRoleMapping>
    {
        public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            builder.ToTable("UserRoleMappings");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasIndex(x => new { x.UserId, x.RoleId }, "UK_UserRoleMappings").IsUnique(); 

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.RoleId).IsRequired();

            builder.HasOne(u => u.User) //3 code step below is for User below
                .WithMany(u => u.UserRolesMappings)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_UserRoleMappings_Users");

            builder.HasOne(r => r.Role)
                .WithMany(r => r.UserRolesMappings)
                .HasForeignKey(r => r.RoleId)
                .HasConstraintName("FK_UserRoleMappings_Roles");
                
        }
    }
} 
