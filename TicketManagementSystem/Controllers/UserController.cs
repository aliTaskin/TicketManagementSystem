using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementSystem.Models;
using TicketManagementSystem.Models.Tables;

namespace TicketManagementSystem.Controllers
{
    public class UserController : Controller
    {
        readonly UserManager<TicketManagementUser> _userManager;// 
        public UserController(UserManager<TicketManagementUser> userManager)//inject dependency
        {
            _userManager = userManager;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(TicketManagementUserViewModel appUserViewModel)
        {
            if (ModelState.IsValid)
            {
                TicketManagementUser appUser = new TicketManagementUser
                {
                    UserName = appUserViewModel.UserName,
                    Email = appUserViewModel.Email
                };
                IdentityResult result = await _userManager.CreateAsync(appUser,appUserViewModel.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            }
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
