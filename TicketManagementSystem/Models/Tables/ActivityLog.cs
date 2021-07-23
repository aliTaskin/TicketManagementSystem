using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketManagementSystem.Data
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public ActionType ActionType { get; set; }

        
        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

    }
}
