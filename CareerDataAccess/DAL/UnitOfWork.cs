using CareerDataAccess.DAL;
using CareerDataAccess.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerDataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(CareerDbContext careerDbContext)
        {
            this.CareerContext = careerDbContext;
        }
        public CareerDbContext CareerContext { get; }

        private IUserRepository userRepository;
        private IJobRepository jobRepository;

        public IUserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(CareerContext);
                }
                return userRepository;
            }
        }
        public IJobRepository JobRepository
        {
            get
            {
                if (this.jobRepository == null)
                {
                    this.jobRepository = new JobRepository(CareerContext);
                }
                return jobRepository;
            }
        }
    }
}
