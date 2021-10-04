using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternProject.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Please enter your Email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
