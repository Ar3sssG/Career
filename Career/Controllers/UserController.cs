using CareerDataAccess.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCommon.CareerViewModels;
using CareerDataAccess.CareerModels;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;

namespace Career.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IUnitOfWork unitOfWork, UserManager<User> userManager, SignInManager<User> signInManager)
            : base(unitOfWork, userManager, signInManager)
        {
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email,
                    NormalizedEmail = model.Email.ToUpper(),
                    NormalizedUserName = model.Email.ToUpper(),
                    RegisterDate = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.Remember, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", CareerCommon.Constants.ErrorsConstants.EmailPasswordError);
                }
            }
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var userClaims = User.FindFirst(ClaimTypes.NameIdentifier);
                    int userID = int.Parse(userClaims.Value);

                    var model = await _unitOfWork.UserRepository.GetUserForProfileView(userID);

                    return View(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View();
                }
               
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProfilePostedJobs(int userId)
        {
            var model = await _unitOfWork.UserRepository.GetUserPostedJobs(userId);

            return PartialView("_PostedJobs",model);
        }

        public async Task<IActionResult> GetProfileApplications(int userId)
        {
            var model = await _unitOfWork.UserRepository.GetUserApplications(userId);

            return PartialView("_JobApplications",model);
        }


        
    }
}
