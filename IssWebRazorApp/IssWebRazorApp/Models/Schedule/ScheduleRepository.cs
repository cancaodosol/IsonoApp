using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models.Schedule
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;

        public ScheduleRepository(IssWebRazorApp.Data.IssWebRazorAppContext context) 
        {
            _context = context;
        }
        public IList<Schedule> FindAll()
        {
            var datas = _context.ScheduleData.ToList();
            IList<Schedule> schedules = new List<Schedule>();
            foreach (var item in datas)
            {
                var schedule = item.ToModel();
                schedules.Add(schedule);
            }
            return schedules;
        }
    }
}
