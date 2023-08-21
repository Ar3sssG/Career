using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerDataAccess.CareerModels
{
    public class Job
    {
        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int Industry { get; set; }
        public int Profession { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Responsibilities { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime DeadLine { get; set; }
        public bool IsActive { get; set; }
    }
}
