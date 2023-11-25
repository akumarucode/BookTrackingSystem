using BookTrackingSystem.Data;
using BookTrackingSystem.Models;
using BookTrackingSystem.Models.viewModels;
using BookTrackingSystem.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookTrackingSystem.Controllers
{
    public class AdminController : Controller
    {

        private readonly IUserSettingRepository userSettingRepository;

        public AdminController(IUserSettingRepository userSettingRepository)
        {
            this.userSettingRepository = userSettingRepository;
        }


        public IActionResult Index()
        {
            return View();
        }


        //[Authorize(Roles = "Admin")]
        //public IActionResult userSetting(string searchString)
        //{

        //    if (User.Identity.IsAuthenticated)
        //    {
        //        ViewData["CurrentFilter"] = searchString;

        //        var searchedBook = from mem in _context.Users
        //                           select mem;
        //        if (!String.IsNullOrEmpty(searchString))
        //        {
        //            searchedBook = searchedBook.Where(m => m.UserName.Contains(searchString)
        //                                   || m.NormalizedUserName.Contains(searchString)
        //                                   || m.Email.Contains(searchString));
        //            return View(searchedBook);
        //        }

        //        var bookList = _context.Users.ToList();
        //        return View(bookList);
        //    }

        //    return RedirectToAction("Restricted", "Restrictions");
        //}


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> userSetting()
        {

            if (User.Identity.IsAuthenticated)
            {

                var userList = await userSettingRepository.DisplayUserSettingAsync();
                return View(userList);
            }

            return RedirectToAction("Restricted", "Restrictions");

        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await userSettingRepository.GetUserAsync(id);
                

                if (user != null)
                {
                    var editUserRequest = new Users
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        NormalizedUserName = user.NormalizedUserName,
                        Email = user.Email
                                         
                    };

                    return View(editUserRequest);
                }

                return View(null);
            }

            return RedirectToAction("Restricted", "Restrictions");
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditUser(Users editUserRequest)
        {
            
            if (ModelState.IsValid)
            {
                var user = new Users { Id = editUserRequest.Id, UserName = editUserRequest.UserName, NormalizedUserName = editUserRequest.NormalizedUserName, Email = editUserRequest.Email};

                    //save change
                    await userSettingRepository.UpdateUserAsync(user);

                    //show success notification
                    return RedirectToAction("Updated");

                
            }
            //show failure notification
            return RedirectToAction("Edit", new { id = editUserRequest.Id });
        }

        [Authorize(Roles = "Admin")]
        //Delete
        [HttpGet]
        public async Task <IActionResult> DeleteUser(string confirm_value, string Id)
        {

            var deleteBook = await userSettingRepository.DeleteUserAsync(Id);
            if (confirm_value == "Yes")
            {
                return RedirectToAction("userSetting", "Admin");
            }
            else
            {
                //error notification
                return RedirectToAction("userSetting", "Admin");
            }

        }


        [Authorize(Roles = "Admin")]
        //redirect to updated page
        public ActionResult Updated()
        {
            TempData["alertMessage"] = "User details updated!";
            return RedirectToAction("userSetting", "Admin");
        }

        [Authorize(Roles = "Admin")]

        //redirect to updated page
        public ActionResult Deleted()
        {
            TempData["alertMessage"] = "User deleted!";
            return RedirectToAction("userSetting", "Admin");
        }




    }
}
