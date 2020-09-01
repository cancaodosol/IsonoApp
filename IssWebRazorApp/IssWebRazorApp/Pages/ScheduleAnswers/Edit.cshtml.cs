using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IssWebRazorApp.Data;
using IssWebRazorApp.Models.Common;
using IssWebRazorApp.Models;
using IssWebRazorApp.Models.LINENotify;

namespace IssWebRazorApp.ScheduleAnswers
{
    public class EditModel : PageModel
    {
        private readonly IssWebRazorApp.Data.IssWebRazorAppContext _context;
        private readonly SessionService _sessionService;
        private readonly LINENotifyService _lineNotifyService;
        public User LoginUser;
        public SelectList AnswerList { get; set; }

        public EditModel(IssWebRazorApp.Data.IssWebRazorAppContext context)
        {
            _context = context;
            _sessionService = new SessionService();
            _lineNotifyService = new LINENotifyService();
            var items = new Dictionary<string, string> { { "参加", "OK" }, { "欠席", "NG" }, { "保留", "HOLD" } };
            AnswerList = new SelectList(items, "Value", "Key");
        }

        [BindProperty]
        public ScheduleAnswerData ScheduleAnswerData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            LoginUser = _sessionService.GetLoginUser(HttpContext);
            if (LoginUser == null) return RedirectToPage("/Login");

            if (id == null)
            {
                return NotFound();
            }

            ScheduleAnswerData = await _context.ScheduleAnswerData.FirstOrDefaultAsync(m => m.ScheduleId == id && m.UserId == LoginUser.UserId);

            if (ScheduleAnswerData == null)
            {
                ScheduleAnswerData = new ScheduleAnswerData();
                ScheduleAnswerData.ScheduleId = (int)id;
                ScheduleAnswerData.UserId = LoginUser.UserId;
                ScheduleAnswerData.CreateDate = DateTime.Now;
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            LoginUser = _sessionService.GetLoginUser(HttpContext);
            if (LoginUser == null) return RedirectToPage("/Login");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ScheduleAnswerData).State = EntityState.Modified;
            ScheduleAnswerData.LastUpdateDate = DateTime.Now;

            var data = this.ScheduleAnswerData;
            var schedule = _context.ScheduleData.Find(ScheduleAnswerData.ScheduleId);

            try
            {
                if (!ScheduleAnswerDataExists(ScheduleAnswerData.ScheduleId, LoginUser.UserId))
                {
                    _context.Add(ScheduleAnswerData);
                    await _context.SaveChangesAsync();
                    var message = "【スケジュール回答】\nスケジュール："+schedule.StartDate.ToString("MM/dd")+":"+schedule.Title+"\n" 
                        + LoginUser.UserName.NameKanji +" は、" + data.Answer + "と回答しました。\nコメント：" + data.Comment;
                    _lineNotifyService.SendMessage(message);
                }
                else
                {
                    await _context.SaveChangesAsync();
                    var message = "【スケジュール回答】\nスケジュール：" + schedule.StartDate.ToString("MM/dd") + ":" + schedule.Title + "\n"
                        + LoginUser.UserName.NameKanji + " は、" + data.Answer + "に変更しました。\nコメント：" + data.Comment;
                    _lineNotifyService.SendMessage(message);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage("/Index");
            }

        private bool ScheduleAnswerDataExists(int scheduleId,int userId)
        {
            return _context.ScheduleAnswerData.Any(e => e.ScheduleId == scheduleId && e.UserId == userId);
        }
    }
}
