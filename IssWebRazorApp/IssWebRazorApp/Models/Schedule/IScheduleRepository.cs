using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models.Schedule
{
    public interface IScheduleRepository
    {
        public IList<Schedule> FindAll();
    }
}
