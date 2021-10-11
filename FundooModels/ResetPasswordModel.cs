using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooModels
{
    public class ResetPasswordModel
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9]+([\.+\-_][A-Za-z0-9]+)*@[a-zA-Z0-9]+\.?[A-Za-z]+\.?[A-Za-z]{2,}$", ErrorMessage = "")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
