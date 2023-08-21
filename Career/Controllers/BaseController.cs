using Microsoft.AspNetCore.Mvc;
using System;
using CareerDataAccess.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using CareerDataAccess.CareerModels;
using Career.Helpers;

namespace Career.Controllers
{
    public class BaseController : Controller
    {
        protected IUnitOfWork _unitOfWork;
        protected UserManager<User> _userManager;
        protected SignInManager<User> _signInManager;
        public BaseController(IUnitOfWork unitOfWork, UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

    }
}
