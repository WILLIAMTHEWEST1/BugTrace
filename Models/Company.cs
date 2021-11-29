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
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Company Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(2500)]
        public string Description { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }

        [DisplayName("Project Image")]
        public string ImageFileName { get; set; }

        public byte[] ImageFileData { get; set; }

        [Display(Name = "File Extension")]
        public string ImageContentType { get; set; }


        //Navigational Properties
        public virtual ICollection<BTUser> Members { get; set; } = new HashSet<BTUser>();
        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
        public virtual ICollection<Invite> Invites { get; set; } = new HashSet<Invite>();


    }
}
