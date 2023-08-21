using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCommon.CareerViewModels
{
    public class JobsViewModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        [Required]
        public int Industry { get; set; }
        public int Profession { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Requirements { get; set; }
        [Required]
        public string Responsibilities { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime PostedDate { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DeadLine { get; set; }
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public string IndustryName { get; set; }
        public string ProfessionName { get; set; }
        public List<JobApplicationsViewModel> Applications;
    }

    public class JobPostViewModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        [Required]
        public int Industry { get; set; }
        [Required]
        [StringLength(70, MinimumLength = 20)]
        public string Title { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 30)]
        public string Description { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 30)]
        public string Requirements { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 30)]
        public string Responsibilities { get; set; }
        public DateTime PostedDate { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DeadLine { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int Profession { get; set; }
        public List<IndustriesViewModel> Industries { get; set; }
    }

}
