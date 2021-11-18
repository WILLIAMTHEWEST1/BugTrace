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
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Company")]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("ProjectName")]
        public int Name { get; set; }

        [Required]
        [StringLength(2500)]
        public string Description { get; set; }

        [DisplayName("Priority")]
        public int? ProjectPriorityId { get; set; }

        [DisplayName("Created Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset CreatedDate { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset? StartDate { get; set; }


        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        public DateTimeOffset? EndDate { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }

        [DisplayName("Project Image")]
        public string ImageFileName { get; set; }

        public byte[] ImageFileData { get; set; }

        [Display(Name = "File Extension")]
        public string ImageContentType { get; set; }

        [DisplayName("Archived")]
        public bool Archived { get; set; }


        //Navigational Properties
        public virtual Company Company { get; set; }
        public virtual ProjectPriority ProjectPriority { get; set; }

        public virtual ICollection<ProjectPriority> Priority { get; set; } = new HashSet<ProjectPriority>();
        public virtual ICollection<Ticket> Ticket { get; set; } = new HashSet<Ticket>();
        public virtual ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();

    }
}
