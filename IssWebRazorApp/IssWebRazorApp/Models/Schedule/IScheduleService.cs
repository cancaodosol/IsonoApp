using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public interface IScheduleService
    {
        public IList<Schedule> FindAll();
        public IList<Schedule> FindRecentry(int day);
        public IList<Schedule> FindRecentry(int day,int userId);
    }
}
