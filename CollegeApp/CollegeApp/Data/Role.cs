namespace CollegeApp.Data
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatededDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ICollection<RolePrivilege> Privileges { get; set; }
        public virtual ICollection<UserRoleMapping> UserRolesMappings { get; set; }
    }
}
