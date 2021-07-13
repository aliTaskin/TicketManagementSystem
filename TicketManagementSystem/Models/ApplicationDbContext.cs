using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicketManagementSystem.Models.Tables;

namespace TicketManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext <TicketManagementUser, TicketManagementRole,int>
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
