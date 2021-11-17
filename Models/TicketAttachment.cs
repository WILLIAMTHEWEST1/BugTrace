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
    public class TicketAttachment
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        [DisplayName("Ticket Task")]
        public int TicketTaskId { get; set; }

        [Required]
        public string UserId { get; set; }

        [DisplayName("Attachment Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset Created { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 7)]
        [DisplayName("Attachment Description")]
        public String Description { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }

        [DisplayName("File Name")]
        public string FileName { get; set; }

        public byte[] FileData { get; set; }

        [Display(Name = "File Extension")]
        public string ContentType { get; set; }


        //Navigational Properties
        public virtual Ticket Ticket { get; set; }

        public virtual BTUser User { get; set; }

    }
}
