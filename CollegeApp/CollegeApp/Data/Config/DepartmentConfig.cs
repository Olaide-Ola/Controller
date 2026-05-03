using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollegeApp.Data.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department");
            builder.HasKey(z => z.Id);
            builder.Property( z => z.Id).UseIdentityColumn();

            builder.Property(z => z.DepartmentName).IsRequired().HasMaxLength(200);
            builder.Property(z => z.Description).IsRequired(false).HasMaxLength(500);

            builder.HasData(new List<Department>()
            {
                new Department { Id = 1, DepartmentName = "ECE", Description = "ECE Department" },
                new Department { Id = 2, DepartmentName = "CSE", Description = "CSE Department" },
            });
        }
    }
}
