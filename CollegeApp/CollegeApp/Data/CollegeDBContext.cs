using CollegeApp.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePrivilege> RolePrivileges { get; set; }
        public DbSet<UserRoleMapping> UserRoleMappings { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Table 1
            modelBuilder.ApplyConfiguration(new StudentConfig());

            //Table 2
            modelBuilder.ApplyConfiguration(new DepartmentConfig());

            //Table 3 - Central
            modelBuilder.ApplyConfiguration(new UserConfig());

            //Table 4
            modelBuilder.ApplyConfiguration(new RoleConfig());

            //Table 5
            modelBuilder.ApplyConfiguration(new RolePrivilegeConfig());

            //Table 6
            modelBuilder.ApplyConfiguration(new UserRoleMappingConfig());

            //Table 7
            modelBuilder.ApplyConfiguration(new UserTypeConfig());

        }
    }
}
