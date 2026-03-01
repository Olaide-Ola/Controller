using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Model
{
    public class Student
    {
        public int Id { get; set; } = 0;

        [Required]
        public string StudentName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
