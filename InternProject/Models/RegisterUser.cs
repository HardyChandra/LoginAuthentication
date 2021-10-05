using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternProject.Models
{
    public class RegisterUser
    {
        [Required(ErrorMessage = "Please enter your fullname.")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "Please enter your email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your password.")]
        [DataType(DataType.Password)]
        [Compare(nameof(ConfirmPassword), ErrorMessage = "Password and confirm password did not match.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter your confirm password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
