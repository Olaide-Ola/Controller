using CollegeApp.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Model
{
    public class StudentDTO
    {
        //[ValidateNever]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(35)]
        public string StudentName { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        //[Required]
        //[Range(10,20)]
        //public int Age { get; set; }

        [Required]
        public string Address { get; set; }

        //[Required]
        //public string Password { get; set; }

        //[Required]
        //[Compare(nameof(Password))]
        //public string ConfirmPassword { get; set; }

        //[DateCheckAttribute]
        //public DateTime AdmissionDate { get; set; }
    }
}
