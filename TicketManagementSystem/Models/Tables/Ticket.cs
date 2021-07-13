using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementSystem.Models.Tables;

namespace TicketManagementSystem.Data
{
    public class Ticket
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Statement { get; set; }
        public virtual TicketManagementUser CreatedBy { get; set; }             //
        public DateTime DateCreated { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public DateTime DateModified { get; set; }
        public Priority Priority { get; set; }
        public virtual TicketManagementUser AssignedTo { get; set; }            //
        public ActivityLog [] ActivityLog { get; set; }
        public string ConclusionText { get; set; }




    }
}
