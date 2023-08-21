using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCommon.Enums
{
    public class JobsEnums
    {
        public enum JobIndustries
        {
            TechnologyAndTelecom = 1,
            Services = 2,
            ManufacturingAndEngineering = 3,
            Bfsi = 4,
            Commerce = 5,
            ConstructionAndEngineering = 6,
            PowerAndEnergy = 7,
            Healthcare = 8,
            Logistics = 9,
            Agriculture = 10,
            Lifestyle = 11,
            Others = 12
        }

        public enum SortType
        {
            ByPostDate = 1,
            ByDeadLine = 2
        }

    }
}
