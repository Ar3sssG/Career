using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerDataAccess.CareerModels
{
    public class JobApplications
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int JobID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
