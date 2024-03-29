﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TicketManagementSystem.Data;

namespace TicketManagementSystem.Models.Tables
{
    public class TicketManagementUser:IdentityUser<int>
    {

        [PersonalData]
        public string Name { get; set; }

        [PersonalData]
        public string Surname { get; set; }

        [PersonalData]
        public DateTime DateOfBirth { get; set; }

        public List <Ticket> Ticket { get; set; }

       


    }
}
