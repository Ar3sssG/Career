using CareerCommon.CareerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerDataAccess.DAL.Interfaces
{
    public interface IJobRepository
    {
        Task<List<JobsViewModel>> GetJobsList(string searchText, List<int> professionType, int sortType);
        Task PostJob(JobPostViewModel model);
        Task<List<IndustriesViewModel>> GetIndustries();
        Task<List<ProfessionsViewModel>> GetProfessions();
        Task<List<ProfessionsViewModel>> GetProfessionsById(int industryID);
        Task<List<IndustriesProfessionsViewModel>> GetIndustriesProfessions();
        Task<JobsViewModel> GetJobById(int jobId);
        Task ApplicationToJob(int jobId, int userId);
        Task JobDeactivate(int jobId);
    }
}
