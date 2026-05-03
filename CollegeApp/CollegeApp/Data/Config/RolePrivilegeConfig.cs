using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class RolePrivilegeConfig : IEntityTypeConfiguration<RolePrivilege>
    {
        public void Configure(EntityTypeBuilder<RolePrivilege> builder)
        {
            builder.ToTable("RolePrivileges");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.RolePrivilegeName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Description).IsRequired(false);
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.CreatededDate).IsRequired();

            builder.HasOne(x => x.Role)
            .WithMany(x => x.Privileges)
            .HasForeignKey(x => x.RoleId)
            .HasConstraintName("FK_RolePrivileges_Roles");
        }
    }
}
