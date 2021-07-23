using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementSystem.Data;
using TicketManagementSystem.Models;
using System.Data.SqlClient;
using System.Data;
using TicketManagementSystem.Models.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TicketManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<TicketManagementUser> _userManager;


        private readonly ApplicationDbContext _context;

     
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<TicketManagementUser> userManager)
        {
            _logger = logger;
            _context = context;// inject dependency
            _userManager = userManager;
        }

        private Task<TicketManagementUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);//get the current logged in user's Id.

        public async Task <IActionResult> Index()
        {

            var user = await GetCurrentUserAsync();

            if (user == null)
            {
               return Redirect("~/Identity/Account/Login");
            }

            var users =await _context.Users.ToListAsync();

            return View(users);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
