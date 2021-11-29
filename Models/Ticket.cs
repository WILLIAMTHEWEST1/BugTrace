using BugTrace.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrace.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName]
        public string Title { get; set; }

        [Required]
        [StringLength(2500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 7)]
        [DisplayName("Ticket Description")]
        public String Description { get; set; }

        [DisplayName("Created Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset Created { get; set; }

        [DisplayName("Updated")]
        [DataType(DataType.Date)]
        public DateTimeOffset Updated { get; set; }

        [DisplayName("Archived")]
        public bool Archived { get; set; }



        [DisplayName("Archived By Project")]
        public bool ArchivedByProject { get; set; }

        [DisplayName("Project")]
        public int ProjectId { get; set; }

        [DisplayName("Ticket Type")]
        public int TicketTypeId { get; set; }

        [DisplayName("Task Priority")]
        public int TicketPriorityId { get; set; }

        [DisplayName("Task Status")]
        public int TicketStatusId { get; set; }

        [DisplayName("Ticket Owner")]
        public string OwnerUserId { get; set; }

        [DisplayName("Ticket Developer")]
        public string DeveloperUserId { get; set; }

        public virtual Project Project { get; set; }
        public virtual TicketType TicketType { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        public virtual BTUser OwnerUser { get; set; }
        public virtual BTUser DeveloperUser { get; set; }
      

        public virtual ICollection<TicketComment> Comments { get; set; } = new HashSet<TicketComment>();
        public virtual ICollection<TicketAttachment> Attachments { get; set; } = new HashSet<TicketAttachment>();
        public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
        public virtual ICollection<TicketHistory> History { get; set; } = new HashSet<TicketHistory>();
        public virtual ICollection<TicketTask> Tasks { get; set; } = new HashSet<TicketTask>();





    }

}
