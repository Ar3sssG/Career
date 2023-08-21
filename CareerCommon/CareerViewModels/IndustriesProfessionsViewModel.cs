using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCommon.CareerViewModels
{
    public class IndustriesProfessionsViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<ProfessionsViewModel> Professions { get; set; }
    }
}
