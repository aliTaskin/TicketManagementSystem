using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketManagementSystem.Models
{
    public class TicketManagementUserViewModel
    {
        [Required(ErrorMessage = "Please Enter User Name")]
        [StringLength(15, ErrorMessage = "Please enter a valid user name", MinimumLength = 4)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter E-Mail")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password, ErrorMessage = "Please enter a valid password")]
        [Display(Name = "Password")]
        public string Sifre { get; set; }
    }
}
