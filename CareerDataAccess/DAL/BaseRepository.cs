using CareerDataAccess.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerDataAccess.DAL
{
    class BaseRepository : IBaseRepository
    {
        protected CareerDbContext CareerDbContext;
    }
}
