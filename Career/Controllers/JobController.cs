using Career.Helpers;
using CareerCommon.CareerViewModels;
using CareerDataAccess.CareerModels;
using CareerDataAccess.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Career.Controllers
{
    public class JobController : BaseController
    {
        public JobController(IUnitOfWork unitOfWork, UserManager<User> userManager, SignInManager<User> signInManager)
            : base(unitOfWork, userManager, signInManager)
        {
        }

        [HttpGet]
        public async Task<IActionResult> JobList(string searchText, List<int> professionType,int sortType)
        {
            if (searchText != null)
            {
                searchText = searchText.Trim();
                if (searchText.Length < 2)
                {
                    searchText = null;
                }
            }
            

            var jobList = await _unitOfWork.JobRepository.GetJobsList(searchText,professionType,sortType);

            return Request.IsAjaxRequest() ? PartialView("_List", jobList) : View("JobList",jobList);
        }

        [HttpGet]
        public async Task<IActionResult> JobPost()
        {
            if (User.Identity.IsAuthenticated)
            {
                var industries = await _unitOfWork.JobRepository.GetIndustries();

                JobPostViewModel postViewModel = new JobPostViewModel()
                {
                    Industries = industries
                };

                return View(postViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public async Task<IActionResult> JobPost(JobPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.JobRepository.PostJob(model);

                    return RedirectToAction("JobList", "Job");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetProfessionsById(int industryId)
        {
            var models = await _unitOfWork.JobRepository.GetProfessionsById(industryId);

            return PartialView("_Profession", models);
        }

        [HttpGet]
        public async Task<IActionResult> GetIndustries()
        {
            var idnustries = await _unitOfWork.JobRepository.GetIndustriesProfessions();

            return PartialView("_IndustriesList", idnustries);
        }

        [HttpGet]
        public async Task<IActionResult> GetProfessions()
        {
            var professions = await _unitOfWork.JobRepository.GetProfessions();

            return PartialView("_ProfessionsList", professions);
        }

        [HttpGet]
        public async Task<IActionResult> GetJobById(int jobId)
        {
            var job = await _unitOfWork.JobRepository.GetJobById(jobId);

            return View("JobDetails",job);
        }

        [HttpGet]
        public async Task<IActionResult> ApplicationToJob(int jobId,int userId)
        {
            try
            {
                await _unitOfWork.JobRepository.ApplicationToJob(jobId, userId);

                var job = await _unitOfWork.JobRepository.GetJobById(jobId);

                return View("JobDetails", job);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("GetJobById",jobId);
            }
        }

        [HttpGet]
        public async Task<IActionResult> JobDeactivate(int jobId)
        {
            try
            {
                await _unitOfWork.JobRepository.JobDeactivate(jobId);
                return RedirectToAction("JobList","Job");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return RedirectToAction("GetJobById", jobId);
            }
        }
    }
}
