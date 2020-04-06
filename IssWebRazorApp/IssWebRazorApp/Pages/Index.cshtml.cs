using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssWebRazorApp.Models.Schedule;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace IssWebRazorApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IScheduleRepository _scheduleRepository;

        public IndexModel(ILogger<IndexModel> logger , IScheduleRepository scheduleRepository)
        {
            _logger = logger;
            _scheduleRepository = scheduleRepository;
        }

        public IList<Schedule> schedules;
        public void OnGet()
        {
            schedules = _scheduleRepository.FindAll().OrderBy(m => m.StartDate).ToList<Schedule>();
        }
    }
}
