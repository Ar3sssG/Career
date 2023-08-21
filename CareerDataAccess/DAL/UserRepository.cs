using CareerCommon.CareerViewModels;
using CareerDataAccess.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerDataAccess.DAL
{
    internal class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(CareerDbContext careerDbContext)
        {
            this.CareerDbContext = careerDbContext;
        }

        public async Task<UserViewModel> GetUserForProfileView(int userId)
        {

            var model = await (from u in CareerDbContext.Users
                               where u.Id == userId
                               select new UserViewModel
                               {
                                   ID = u.Id,
                                   FullName = $"{u.FirstName} {u.LastName}",
                                   RegisterDate = u.RegisterDate
                               }).SingleOrDefaultAsync();

            var jobsLst = await (from j in CareerDbContext.Jobs
                                 where j.UserID == userId
                                 select new JobsViewModel
                                 {
                                     ID = j.ID,
                                     UserID = j.UserID,
                                     Industry = j.Industry,
                                     Title = j.Title,
                                     Description = j.Description,
                                     Requirements = j.Requirements,
                                     Responsibilities = j.Responsibilities,
                                     PostedDate = j.PostedDate,
                                     DeadLine = j.DeadLine,
                                     IsActive = j.IsActive
                                 }).ToListAsync();

            var applicList = await (from a in CareerDbContext.JobsApplications
                                    where a.UserID == userId
                                    select new JobApplicationsViewModel
                                    {
                                        ID = a.ID,
                                        JobID = a.JobID,
                                        UserID = a.UserID,
                                        ApplicationDate = a.ApplicationDate,
                                        IsActive = a.IsActive,

                                    }).ToListAsync();

            foreach (var item in applicList)
            {
                item.AppliedJob = jobsLst.Where(x => x.ID == item.JobID && x.IsActive == true).SingleOrDefault();
            }

            model.PostedJobs = jobsLst;
            model.SubmittedApplications = applicList;

            //var model = await (from u in CareerDbContext.Users
            //                   join j in CareerDbContext.Jobs on u.Id equals j.UserID into jj
            //                   from jjj in jj.DefaultIfEmpty()
            //                   join a in CareerDbContext.JobsApplications on u.Id equals a.UserID into ja
            //                   from jja in ja.DefaultIfEmpty()
            //                   where u.Id == userId
            //                   group new { user = u, job = jjj, application = jja } by u.Id into g

            //                   select new UserViewModel
            //                   {
            //                       //FullName = $"{g.First().user.FirstName} {g.First().user.LastName}",
            //                       //RegisterDate = g.First().user.RegisterDate,
            //                       PostedJobs = g.Select(x => x.job) != null ? g.Select(x => new JobsViewModel
            //                       {
            //                           ID = x.job.ID,
            //                           UserID = x.job.UserID,
            //                           Industry = x.job.Industry,
            //                           Title = x.job.Title,
            //                           Description = x.job.Description,
            //                           Requirements = x.job.Requirements,
            //                           Responsibilities = x.job.Responsibilities,
            //                           PostedDate = x.job.PostedDate,
            //                           DeadLine = x.job.DeadLine,
            //                           IsActive = x.job.IsActive
            //                       }).ToList() : new List<JobsViewModel>(),
            //                       SubmittedApplications = g.Select(x => x.application) != null ? g.Select(x => new JobApplicationsViewModel
            //                       {
            //                           ID = x.application.ID,
            //                           UserID = x.application.UserID,
            //                           JobID = x.application.JobID,
            //                           ApplicationDate = x.application.ApplicationDate,
            //                           IsActive = x.application.IsActive
            //                       }).ToList() : new List<JobApplicationsViewModel>()
            //                   }).SingleOrDefaultAsync();

            return model;
        }

        public async Task<List<JobApplicationsViewModel>> GetUserApplications(int userId)
        {
            var applications = await (from a in CareerDbContext.JobsApplications
                                      where a.UserID == userId
                                      select new JobApplicationsViewModel
                                      {
                                          ID = a.ID,
                                          UserID = a.UserID,
                                          JobID = a.JobID,
                                          ApplicationDate = a.ApplicationDate,
                                          IsActive = a.IsActive
                                      }).ToListAsync();


            var jobs = await (from j in CareerDbContext.Jobs
                              join i in CareerDbContext.JobsIndustries on j.Industry equals i.ID
                              join p in CareerDbContext.Professions on j.Profession equals p.ID

                              select new JobsViewModel
                              {
                                  ID = j.ID,
                                  Title = j.Title,
                                  Description = j.Description,
                                  Requirements = j.Requirements,
                                  Responsibilities = j.Responsibilities,
                                  IndustryName = i.Name,
                                  ProfessionName = p.Name,
                              }).ToListAsync();


            foreach (var item in applications)
            {
                item.AppliedJob = jobs.Where(j => j.ID == item.JobID).SingleOrDefault();
            }

            return applications;
        }

        public async Task<List<JobsViewModel>> GetUserPostedJobs(int userId)
        {
            try
            {
                var jobs = await (from j in CareerDbContext.Jobs
                                  join i in CareerDbContext.JobsIndustries on j.Industry equals i.ID
                                  join p in CareerDbContext.Professions on j.Profession equals p.ID

                                  where j.UserID == userId
                                  select new JobsViewModel
                                  {
                                      ID = j.ID,
                                      Title = j.Title,
                                      Description = j.Description,
                                      Requirements = j.Requirements,
                                      Responsibilities = j.Responsibilities,
                                      IndustryName = i.Name,
                                      ProfessionName = p.Name,
                                      PostedDate = j.PostedDate,
                                      DeadLine = j.DeadLine,
                                      IsActive = j.IsActive
                                  }).ToListAsync();

                var applications = await (from a in CareerDbContext.JobsApplications
                                          select new JobApplicationsViewModel
                                          {
                                              ID = a.ID,
                                              UserID = a.UserID,
                                              JobID = a.JobID,
                                              ApplicationDate = a.ApplicationDate,
                                          }).ToListAsync();

                foreach (var item in jobs)
                {
                    item.Applications = applications.Where(a => a.JobID == item.ID).ToList();
                }


                return jobs;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
