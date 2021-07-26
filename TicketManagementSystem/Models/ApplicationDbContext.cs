using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicketManagementSystem.Models.Tables;
using System.Linq;
using TicketManagementSystem.Data;


namespace TicketManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext <TicketManagementUser, TicketManagementRole,int>
    {

        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActivityLog>()
                 .HasOne(p => p.Ticket)
                 .WithMany(b => b.ActivityLog)
                 .HasForeignKey(p => p.TicketId);

            builder.Entity<Ticket>()
                .HasOne(p => p.AssignedTo)
                .WithMany(b => b.Ticket)
                .HasForeignKey(p => p.AssignedToId);

                
          
        }
    }
}
