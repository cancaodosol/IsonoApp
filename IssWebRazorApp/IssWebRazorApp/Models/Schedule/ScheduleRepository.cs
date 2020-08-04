using IssWebRazorApp.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssWebRazorApp.Models
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;

        public ScheduleRepository(IssWebRazorApp.Data.IssWebRazorAppContext context) 
        {
            _context = context;
        }
        public IList<ScheduleData> FindAll()
        {
            var datas = _context.ScheduleData.ToList();
            return datas;
        }

        public IList<ScheduleData> FindRecently(int day)
        {
            var datas = _context.ScheduleData.Where(_ => _.StartDate >= DateTime.Now.AddDays(-1 * day)).ToList();
            return datas;
        }

        public IList<ScheduleAnswerData> GetScheduleAnswers(int scheduleId) 
        {
            var datas = _context.ScheduleAnswerData.Where(_ => _.ScheduleId == scheduleId).ToList();
            return datas;
        }
    }
}
