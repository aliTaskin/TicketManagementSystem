using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementSystem.Models.Tables;

namespace TicketManagementSystem.Controllers
{
    public class UserController : Controller
    {
        readonly UserManager<TicketManagementUser> _userManager;
        public UserController(UserManager<TicketManagementUser> userManager)//inject dependency
        {
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
