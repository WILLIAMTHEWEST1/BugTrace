using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrace.Models
{
    public class TicketHistory
    {
        public int Id { get; set; }

        [DisplayName("Updated Item")]
        public string Property { get; set; }

        [DisplayName("Previous")]
        public string OldValue { get; set; }

        [DisplayName("Current")]
        public string NewValue { get; set; }

        [DisplayName("Date Modified")]
        [DataType(DataType.Date)]
        public DateTimeOffset Created { get; set; }

        [DisplayName("Change Description")]
        public string Description { get; set; }

        [DisplayName("Ticket")]
        public string TicketId { get; set; }

        [DisplayName("Team Member")]
        public string UserId { get; set; }

        //Navigational Properties
        public virtual Ticket Ticket { get; set; }

        public virtual BTUser User { get; set; }


    }
}
