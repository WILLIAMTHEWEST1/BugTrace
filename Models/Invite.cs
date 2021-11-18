﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrace.Models
{
    public class Invite
    {
        public int Id { get; set; }

        [DisplayName("Invite Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset InviteDate { get; set; }

        [DisplayName("Join Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset JoinDate { get; set; }

        [DisplayName ("Code")]
        public Guid CompanyToken { get; set; }

        [DisplayName("Company")]
        public int CompanyId { get; set; }

        [DisplayName("Project")]
        public int ProjectId { get; set; }

        [DisplayName("Invitor")]
        public string InvitorId { get; set; }

        [DisplayName("Invitee")]
        public string InviteeId { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string InviteeEmail { get; set; }

        [DisplayName("First Name")]
        public string InviteeFirstName { get; set; }

        [DisplayName("Last Name")]
        public string InviteeLastName { get; set; }

        [DisplayName("Message")]
        public string Message { get; set; }

        public bool IsValid { get; set; }

        //Navigational Properties
        public virtual Company Company { get; set; }

        public virtual BTUser Invitor { get; set; }

        public virtual BTUser Invitee { get; set; }

        public virtual Project Project{ get; set; }

    }
}
