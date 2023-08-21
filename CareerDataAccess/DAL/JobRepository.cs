using CareerDataAccess.DAL.Interfaces;
using CareerCommon.CareerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CareerDataAccess.CareerModels;
using static CareerCommon.Enums.JobsEnums;

namespace CareerDataAccess.DAL
{
    class JobRepository : BaseRepository, IJobRepository
    {
        public JobRepository(CareerDbContext careerDbContext)
        {
            this.CareerDbContext = careerDbContext;
        }

        public async Task<List<JobsViewModel>> GetJobsList(string searchText, List<int> professionType, int sortType)
        {
            var jobs = await (from j in CareerDbContext.Jobs
                              join u in CareerDbContext.Users on j.UserID equals u.Id
                              join i in CareerDbContext.JobsIndustries on j.Industry equals i.ID
                              join p in CareerDbContext.Professions on j.Profession equals p.ID

                              where (j.IsActive == true) && (string.IsNullOrEmpty(searchText) || j.Title.ToLower().Contains(searchText.ToLower()))
                              && ((professionType.Count == 0) || (professionType.Contains(j.Profession)))
                              select new JobsViewModel
                              {
                                  ID = j.ID,
                                  UserID = j.UserID,
                                  Industry = j.Industry,
                                  Profession = j.Profession,
                                  Title = j.Title,
                                  Description = j.Description,
                                  Requirements = j.Requirements,
                                  Responsibilities = j.Responsibilities,
                                  PostedDate = j.PostedDate,
                                  DeadLine = j.DeadLine,
                                  IsActive = j.IsActive,
                                  UserName = $"{u.FirstName} {u.LastName}",
                                  IndustryName = i.Name,
                                  ProfessionName = p.Name,
                              }).ToListAsync();

            if (sortType == (int)SortType.ByDeadLine)
            {
                jobs = jobs.OrderBy(x => x.PostedDate).ToList();
            }
            else
            {
                jobs = jobs.OrderByDescending(x => x.PostedDate).ToList();
            }
            return jobs;
        }
        public async Task PostJob(JobPostViewModel model)
        {

            Job job = new Job()
            {
                Title = model.Title,
                Description = model.Description,
                Requirements = model.Requirements,
                Responsibilities = model.Responsibilities,
                Industry = model.Industry,
                Profession = model.Profession,
                UserID = model.UserID,
                PostedDate = DateTime.Now,
                DeadLine = model.DeadLine,
                IsActive = true
            };

            await CareerDbContext.Jobs.AddAsync(job);

            await CareerDbContext.SaveChangesAsync();
        }
        public async Task<List<IndustriesViewModel>> GetIndustries()
        {
            var industries = await (from i in CareerDbContext.JobsIndustries
                                    where i.IsActive == true
                                    select new IndustriesViewModel
                                    {
                                        ID = i.ID,
                                        Name = i.Name,
                                        Description = i.Description
                                    }).ToListAsync();

            return industries;
        }
        public async Task<List<ProfessionsViewModel>> GetProfessions()
        {
            var professions = await (from p in CareerDbContext.Professions
                                     where p.IsActive == true
                                     select new ProfessionsViewModel
                                     {
                                         ID = p.ID,
                                         Name = p.Name,
                                         Description = p.Description
                                     }).ToListAsync();

            return professions;
        }
        public async Task<List<ProfessionsViewModel>> GetProfessionsById(int industryID)
        {
            var professions = await (from p in CareerDbContext.Professions
                                     where p.IsActive == true && industryID == p.IndustryID
                                     select new ProfessionsViewModel
                                     {
                                         ID = p.ID,
                                         Name = p.Name,
                                         Description = p.Description
                                     }).ToListAsync();

            return professions;
        }
        public async Task<List<IndustriesProfessionsViewModel>> GetIndustriesProfessions()
        {


            var industries = await (from i in CareerDbContext.JobsIndustries
                                    where i.IsActive == true

                                    select new IndustriesProfessionsViewModel
                                    {
                                        ID = i.ID,
                                        Name = i.Name,
                                    }).ToListAsync();

            var professions = await (from p in CareerDbContext.Professions
                                     where p.IsActive == true
                                     select new ProfessionsViewModel
                                     {
                                         ID = p.ID,
                                         Name = p.Name,
                                         IndustryID = p.IndustryID
                                     }).ToListAsync();

            foreach (var item in industries)
            {
                item.Professions = professions.Where(x => x.IndustryID == item.ID).ToList();
            }

            return industries;
        }
        public async Task<JobsViewModel> GetJobById(int jobId)
        {
            var job = await (from j in CareerDbContext.Jobs
                             join u in CareerDbContext.Users on j.UserID equals u.Id
                             join i in CareerDbContext.JobsIndustries on j.Industry equals i.ID
                             join p in CareerDbContext.Professions on j.Profession equals p.ID

                             where jobId == j.ID
                             select new JobsViewModel
                             {
                                 ID = j.ID,
                                 UserID = j.UserID,
                                 Industry = j.Industry,
                                 Profession = j.Profession,
                                 Title = j.Title,
                                 Description = j.Description,
                                 Requirements = j.Requirements,
                                 Responsibilities = j.Responsibilities,
                                 PostedDate = j.PostedDate,
                                 DeadLine = j.DeadLine,
                                 IsActive = j.IsActive,
                                 UserName = $"{u.FirstName} {u.LastName}",
                                 IndustryName = i.Name,
                                 ProfessionName = p.Name
                             }).SingleOrDefaultAsync();

            var applications = await (from a in CareerDbContext.JobsApplications
                                      where a.IsActive == true
                                      select new JobApplicationsViewModel
                                      {
                                          ID = a.ID,
                                          UserID = a.UserID,
                                          JobID = a.JobID
                                      }).ToListAsync();

            job.Applications = applications.Where(x => x.JobID == job.ID).ToList();
            return job;
        }
        public async Task ApplicationToJob(int jobId, int userId)
        {
            if (!CareerDbContext.JobsApplications.Any(x => x.UserID == userId && x.JobID == jobId))
            {
                JobApplications application = new JobApplications()
                {
                    UserID = userId,
                    JobID = jobId,
                    ApplicationDate = DateTime.Now,
                    IsActive = true
                };


                await CareerDbContext.JobsApplications.AddAsync(application);
                await CareerDbContext.SaveChangesAsync();
            }

        }
        public async Task JobDeactivate(int jobId)
        {
            var job = await (from j in CareerDbContext.Jobs
                             where j.ID == jobId
                             select j).SingleOrDefaultAsync();

            job.IsActive = false;

            await CareerDbContext.SaveChangesAsync();
        }
    }
}
