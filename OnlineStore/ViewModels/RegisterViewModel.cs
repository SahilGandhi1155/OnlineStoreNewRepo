using Microsoft.AspNetCore.Mvc;
using OnlineStore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Remote(action: "IsEmailInUse", controller: "Account")]
        [EmailAddress]
        [ValidEmailDomainAttribute(allowedDomain: "xyz.com", ErrorMessage = "Email Domain must be xyz.com")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
