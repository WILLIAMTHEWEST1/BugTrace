using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrace.Models
{
    public class Notification
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Notification Type Id")]
        public int NotificationTypeId { get; set; }

        [Required]
        [DisplayName("Message")]
        public string Message { get; set; }

        [DisplayName("Created Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset CreatedDate { get; set; }

        [DisplayName("Has Been Viewed")]
        public bool Viewed { get; set; }

        [DisplayName("Ticket")]
        public int? TicketId { get; set; }

        [DisplayName("Project")]
        public int? ProjectId { get; set; }

        [Required]
        [DisplayName("Recipent")]
        public string RecipeintId { get; set; }

        [Required]
        [DisplayName("Sender")]
        public string SenderId { get; set; }


        //Navigational Properties

        public virtual NotificationType NotificationType { get; set; }
        public virtual Ticket Ticket { get; set; }
        public virtual Project Project { get; set; }
        public virtual BTUser Recipient { get; set; }
        public virtual BTUser Sender { get; set; }
    }
}
