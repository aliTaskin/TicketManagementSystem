using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketManagementSystem.Data
{
    public class ActivityLog
    {
        public int Id { get; set; }
        
        public string?  ActionTakerName { get; set; }
        public string?  ActionTakerSurname { get; set; }
        public string?  AssignedToName { get; set; }
        public string?  AssignedToSurname { get; set; }
        public DateTime Date { get; set; }
        public ActionType ActionType { get; set; }

        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

    }
}
