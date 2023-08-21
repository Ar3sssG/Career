using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCommon.CareerViewModels
{
    public class JobApplicationsViewModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int JobID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public bool IsActive { get; set; }
        public JobsViewModel AppliedJob { get; set; }
    }
}
