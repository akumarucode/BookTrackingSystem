using BookTrackingSystem.Data.Migrations;
using BookTrackingSystem.Models;
using BookTrackingSystem.Models.viewModels;
using BookTrackingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookTrackingSystem.Controllers
{
    public class roleSettingController : Controller
    {

        private readonly IRoleRepository roleRepository;

        public roleSettingController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> roleSetting()
        {

            if (User.Identity.IsAuthenticated)
            {

                var roleList = await roleRepository.DisplayRoleAsync();
                return View(roleList);
            }

            return RedirectToAction("Restricted", "Restrictions");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var role = await roleRepository.GetRoleAsync(id);


                if (role != null)
                {
                    var editRoleRequest = new Role
                    {
                        Id = role.Id,
                        Name = role.Name,
                        NormalizedName = role.NormalizedName,
                        ConcurrencyStamp = role.ConcurrencyStamp

                    };

                    return View(editRoleRequest);
                }

                return View(null);
            }

            return RedirectToAction("Restricted", "Restrictions");
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditRole(Role editRoleRequest)
        {

            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var role = new Role
                    {
                        Id = editRoleRequest.Id,
                        Name = editRoleRequest.Name,
                        NormalizedName = editRoleRequest.NormalizedName,
                        ConcurrencyStamp = editRoleRequest.ConcurrencyStamp

                    };

                    var updatedBook = await roleRepository.UpdateRoleAsync(role);

                    if (updatedBook != null)
                    {

                        //show success notification
                        return RedirectToAction("Updated");

                    }

                    else
                    {
                        return null;
                    }
                }
                //show failure notification
                return RedirectToAction("Edit", new { id = editRoleRequest.Id });
            }
            return null;
        }


        [Authorize(Roles = "Admin")]
        //Delete
        [HttpGet]
        public async Task<IActionResult> DeleteRole(string confirm_value, string Id)
        {

            var deleteBook = await roleRepository.DeleteRoleAsync(Id);
            if (confirm_value == "Yes")
            {
                return RedirectToAction("roleSetting", "RoleSetting");
            }
            else
            {
                //error notification
                return RedirectToAction("roleSetting", "RoleSetting");
            }

        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddRole()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

            return RedirectToAction("Restricted", "Restrictions");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddRole(IdentityRole roleDetails)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    roleRepository.AddRoleAsync(roleDetails);
                    return RedirectToAction("roleSetting", "RoleSetting");
                }

                return View(roleDetails);

            }
            return RedirectToAction("Restricted", "Restrictions");
        }



        [Authorize(Roles = "Admin")]
        //redirect to updated page
        public ActionResult Updated()
        {
            TempData["alertMessage"] = "Role details updated!";
            return RedirectToAction("roleSetting", "RoleSetting");
        }

        [Authorize(Roles = "Admin")]

        //redirect to updated page
        public ActionResult Deleted()
        {
            TempData["alertMessage"] = "Role deleted!";
            return RedirectToAction("roleSetting", "RoleSetting");
        }


    }
}
