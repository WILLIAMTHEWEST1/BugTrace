using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrace.Models.ViewModels
{
    public class ManagedUserRolesViewModel
    {
        public BTUser BTUser { get; set; }

        public MultiSelectList Roles { get; set; }

        public List<string> SelectedRoles { get; set; }



    }
}
