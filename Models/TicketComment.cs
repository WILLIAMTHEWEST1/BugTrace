using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrace.Models
{
    public class TicketComment
    {
        public int Id { get; set; }

        [DisplayName("Member comment")]
        [StringLength(2500, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 25)]
        public String Comment { get; set; }

        [DisplayName("Comment Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset Created { get; set; }

        [DisplayName("Ticket")]
        public string TicketId { get; set; }

        [DisplayName("Team Member")]
        public string UserId { get; set; }

        //Navigational Properties

        public virtual Ticket Ticket { get; set; }

        public virtual BTUser User { get; set; }
    }
}


