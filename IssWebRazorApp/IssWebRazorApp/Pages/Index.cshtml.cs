using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssWebRazorApp.Models;
using IssWebRazorApp.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace IssWebRazorApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IScheduleService _scheduleService;
        private readonly SessionService _sessionService;
        public User LoginUser;

        public IndexModel(ILogger<IndexModel> logger , IScheduleRepository scheduleRepository,IUserRepository userRepository)
        {
            _logger = logger;
            _scheduleService =  new ScheduleService(scheduleRepository,userRepository);
            _sessionService = new SessionService();
        }

        public IList<Schedule> schedules;
        public void OnGet()
        {
            LoginUser = _sessionService.GetLoginUser(HttpContext);
            var userId = LoginUser != null ? LoginUser.UserId : -1;
            schedules = _scheduleService.FindRecentry(30,userId).OrderBy(m => m.StartDate).ToList<Schedule>();
            foreach (var schedule in schedules)
            {
                schedule.SortAnswersByUserPosition();
            }
        }
    }
}
