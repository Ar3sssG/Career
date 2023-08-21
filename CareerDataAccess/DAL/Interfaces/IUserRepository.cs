using System;
using CareerCommon.CareerViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerDataAccess.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<UserViewModel> GetUserForProfileView(int userId);
        Task<List<JobApplicationsViewModel>> GetUserApplications(int userId);
        Task<List<JobsViewModel>> GetUserPostedJobs(int userId);
    }
}
