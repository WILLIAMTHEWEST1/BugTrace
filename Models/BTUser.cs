using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrace.Models
{
    public class BTUser : IdentityUser
    {
        [Required]
        [DisplayName("First Name")]
        [StringLength(40)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [StringLength(40)]
        public string LastName { get; set; }

        [NotMapped]
        [DisplayName("Full Name")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        //This is the image properties needed.

        [NotMapped]
        [DataType(DataType.Upload)]
        public IFormFile ImageFile { get; set; }

        [DisplayName("Avatar")]
        public string AvatarFileName { get; set; }

        public byte[] AvatarFileData { get; set; }

        [Display(Name = "File Extension")]
        public string AvatarContentType { get; set; }

        public int CompanyId { get; set; }


        //Navigational Properties
        public virtual Company Company { get; set; }


        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();

    }
}
