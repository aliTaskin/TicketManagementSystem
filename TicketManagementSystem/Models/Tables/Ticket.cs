using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementSystem.Models.Tables;

namespace TicketManagementSystem.Data
{
    public class Ticket
    {

        

        // DateCreated and DateModified column values are determined by database triggers.When a ticket record is created or update,a trigger is executed

        public int Id { get; set; }
        public string Title { get; set; }
        public string Statement { get; set; }

        /// <summary>
        /// /////////////////////////////////////////
        /// </summary>

        
        public virtual TicketManagementUser CreatedBy { get; set; }

        public int ? AssignedToId { get; set; }
       
        public virtual TicketManagementUser AssignedTo { get; set; }


        /// <summary>
        /// /////////////////////////////////////////
        /// </summary>

        public DateTime DateCreated { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public DateTime DateModified { get; set; }
        public Priority Priority { get; set; }           
        public List <ActivityLog>  ActivityLog { get; set; }
        public string ConclusionText { get; set; }


    }
}
