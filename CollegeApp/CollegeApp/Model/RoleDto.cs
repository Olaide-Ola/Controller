using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Model
{
    public class RoleDto
    {        
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
