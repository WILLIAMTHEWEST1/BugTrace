using BugTrace.Extensions;
using BugTrace.Models;
using BugTrace.Models.ViewModels;
using BugTrace.Services;
using BugTrace.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrace.Controllers
{

    [Authorize(Roles = "Admin")]


    public class UserRolesController : Controller
    {
        private readonly IBTRolesService _rolesService;
        private readonly IBTCompanyInfoService _companyInfoService;
        private readonly UserManager<BTUser> _userManager;

        public UserRolesController(IBTRolesService roleService,
                                   IBTCompanyInfoService companyInfoService,
                                   UserManager<BTUser> userManager)
        {
            _rolesService = roleService;
            _companyInfoService = companyInfoService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ManagedUserRoles()
        {
            //add an instance of the viewmodel as a  list
            List<ManagedUserRolesViewModel> model = new List<ManagedUserRolesViewModel>();

            //Get company Id
            int companyId = User.Identity.GetCompanyId().Value;

            //Get company users
            List<BTUser> users = await _companyInfoService.GetAllMembersAsync(companyId);


            //Loop over the users to populate viewmodel
            //instantiate ViewModel
            //Use _roleServices
            //Create multi-selects
            foreach (BTUser user in users)
            {
                ManagedUserRolesViewModel viewModel = new ManagedUserRolesViewModel();
                viewModel.BTUser = user;
                IEnumerable<string> selected = await _rolesService.GetUserRolesAsync(user);
                viewModel.Roles = new MultiSelectList(await _rolesService.GetRolesAsync(), "Name", "Name", selected);
                model.Add(viewModel);

            }

            //return the model to the view
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManagedUserRoles(ManagedUserRolesViewModel member)
        {
            //instantiate the BTUser
            int companyId = (await _userManager.GetUserAsync(User)).CompanyId;

            BTUser btUser = (await _companyInfoService.GetAllMembersAsync(companyId)).FirstOrDefault(u => u.Id == member.BTUser.Id);

            //Get Roles for User
            IEnumerable<string> roles = (await _rolesService.GetUserRolesAsync(btUser));

            string userRole = member.SelectedRoles.FirstOrDefault();

            //Remove them from their roles
            if (!string.IsNullOrEmpty(userRole))
            {
                if (await _rolesService.RemoveUserFromRolesAsync(btUser, roles))
                {

                    await _rolesService.AddUserToRoleAsync(btUser, userRole);

                }
                //Navigate back to the Index
                
            }
            return RedirectToAction(nameof(ManagedUserRoles));
        }
    }
}
