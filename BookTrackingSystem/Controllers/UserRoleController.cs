using BookTrackingSystem.Data;
using BookTrackingSystem.Models;
using BookTrackingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Linq;

namespace BookTrackingSystem.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly IRoleRepository roleRepository;

        public UserRoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> UserRoleSetting()
        {
            var roles = await roleRepository.DisplayRoleAsync();

            var users = await roleRepository.DisplayUserSettingAsync();

            var model = new Users
            {
                Roles = roles.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                Userslist = users.Select(x => new SelectListItem { Text = x.UserName, Value = x.Id.ToString() })
            };

            return View(model);
        }
         
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UserRoleSetting(IdentityUserRole<string?> userDetails )
        {

            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {   
                   
                    
                    await roleRepository.SetUserRole(userDetails);
                    return RedirectToAction("Updated");
                }

                return View(userDetails);


            }

            return RedirectToAction("Restricted", "Restrictions");
        }

        [Authorize(Roles = "Admin")]
        //redirect to updated page
        public ActionResult Updated()
        {
            TempData["alertMessage"] = "User role updated!";
            return RedirectToAction("UserRoleSetting", "UserRole");
        }

    }
}
