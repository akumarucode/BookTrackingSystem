using BookTrackingSystem.Models;
using BookTrackingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookTrackingSystem.Controllers
{
    public class DisplayRolesController : Controller
    {
        private readonly IRoleRepository roleRepository;

        public DisplayRolesController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await roleRepository.DisplayRoleAsync();

            var users = await roleRepository.DisplayUserSettingAsync();

            var model = new Users
            {
                Roles = roles.Select(x => new SelectListItem { Text = x.Name,Value = x.Id.ToString()}),
                Userslist = users.Select(x => new SelectListItem { Text = x.UserName,Value = x.Id.ToString()})
            };

            return View(model);
        }
    }
}
